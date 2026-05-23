const inputIds = ["heightCm", "weightKg", "age", "gender", "activityLevel"]

const API_BASE_URL = (() => {
  if (window.location.protocol === "file:") {
    return "http://localhost:5051"
  }

  return window.location.origin
})()

const elements = {
  heightCm: document.querySelector("#heightCm"),
  weightKg: document.querySelector("#weightKg"),
  age: document.querySelector("#age"),
  gender: document.querySelector("#gender"),
  activityLevel: document.querySelector("#activityLevel"),
  bmiButton: document.querySelector("#bmiButton"),
  bmrButton: document.querySelector("#bmrButton"),
  tdeeButton: document.querySelector("#tdeeButton"),
  resultText: document.querySelector("#resultText"),
  apiErrorText: document.querySelector("#apiErrorText")
}

const parseNumber = (value) => {
  if (value === "") {
    return null
  }

  const parsed = Number(value)
  return Number.isFinite(parsed) ? parsed : null
}

const collectState = () => {
  return {
    heightCm: parseNumber(elements.heightCm.value),
    weightKg: parseNumber(elements.weightKg.value),
    age: parseNumber(elements.age.value),
    gender: elements.gender.value || null,
    activityLevel: elements.activityLevel.value || null
  }
}

const validate = (state) => {
  const errors = {}

  if (state.heightCm !== null && state.heightCm <= 0) {
    errors.heightCm = "身高必須大於 0"
  }

  if (state.weightKg !== null && state.weightKg <= 0) {
    errors.weightKg = "體重必須大於 0"
  }

  if (state.age !== null && (!Number.isInteger(state.age) || state.age < 1 || state.age > 120)) {
    errors.age = "年齡需為 1 到 120 的整數"
  }

  if (state.gender !== null && state.gender !== "Male" && state.gender !== "Female") {
    errors.gender = "性別資料不正確"
  }

  const allowLevels = ["Sedentary", "Light", "Moderate", "High", "VeryHigh"]
  if (state.activityLevel !== null && !allowLevels.includes(state.activityLevel)) {
    errors.activityLevel = "活動量資料不正確"
  }

  return errors
}

const renderFieldErrors = (errors) => {
  inputIds.forEach((id) => {
    const errorElement = document.querySelector(`#${id}Error`)
    if (errorElement) {
      errorElement.textContent = errors[id] ?? ""
    }
  })
}

const hasValue = (value) => value !== null

const refreshButtons = () => {
  const state = collectState()
  const errors = validate(state)

  renderFieldErrors(errors)

  const hasBlockingError = Object.keys(errors).length > 0

  const bmiReady = !hasBlockingError && hasValue(state.heightCm) && hasValue(state.weightKg)
  const bmrReady = bmiReady && hasValue(state.age) && hasValue(state.gender)
  const tdeeReady = bmrReady && hasValue(state.activityLevel)

  elements.bmiButton.disabled = !bmiReady
  elements.bmrButton.disabled = !bmrReady
  elements.tdeeButton.disabled = !tdeeReady
}

const showResult = (text) => {
  elements.resultText.textContent = text
  elements.resultText.classList.remove("result-pop")
  void elements.resultText.offsetWidth
  elements.resultText.classList.add("result-pop")
}

const showApiError = (text) => {
  elements.apiErrorText.textContent = text
  if (text) {
    elements.apiErrorText.classList.remove("result-pop")
    void elements.apiErrorText.offsetWidth
    elements.apiErrorText.classList.add("result-pop")
  }
}

const postJson = async (url, payload) => {
  const response = await fetch(new URL(url, API_BASE_URL), {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(payload)
  })

  const body = await response.json()
  if (!response.ok) {
    const message = body?.message ?? "API 呼叫失敗"
    throw new Error(message)
  }

  return body
}

const buildPayload = () => {
  const state = collectState()
  return {
    heightCm: state.heightCm,
    weightKg: state.weightKg,
    age: state.age,
    gender: state.gender,
    activityLevel: state.activityLevel
  }
}

const calculateMetric = async (metric) => {
  showApiError("")
  showResult("計算中...")

  const payload = buildPayload()
  let endpoint = ""

  if (metric === "BMI") {
    endpoint = "/api/health/bmi"
  } else if (metric === "BMR") {
    endpoint = "/api/health/bmr"
  } else {
    endpoint = "/api/health/tdee"
  }

  try {
    const result = await postJson(endpoint, payload)
    showResult(`${result.metric}: ${result.value} ${result.unit}`)
  } catch (error) {
    const message = error instanceof Error ? error.message : "未知錯誤"
    showApiError(message)
    showResult("計算失敗")
  }
}

inputIds.forEach((id) => {
  const element = elements[id]
  if (!element) {
    return
  }

  element.addEventListener("input", refreshButtons)
  element.addEventListener("change", refreshButtons)
})

elements.bmiButton.addEventListener("click", () => calculateMetric("BMI"))
elements.bmrButton.addEventListener("click", () => calculateMetric("BMR"))
elements.tdeeButton.addEventListener("click", () => calculateMetric("TDEE"))

refreshButtons()