---
description: "Use when writing or refactoring JavaScript files. Enforces JavaScript code style, naming, module usage, error handling, and async patterns for consistent maintainable code."
name: "JavaScript Code Style"
applyTo: "**/*.js, **/*.mjs, **/*.cjs"
---
# JavaScript 程式碼規範

## 目標
- 提升可讀性、一致性與可維護性。
- 優先可預期、易測試、易除錯的寫法。
- 本規範以本專案自訂規則為準，不強制對齊外部風格指南。

## 格式一致性
- 分號不強制，但同一檔案內需保持一致。
- 盡量避免同一程式區塊混用有分號與無分號寫法。

## 語言與模組
- 預設使用 ES Modules（`import` / `export`），除非專案明確使用 CommonJS。
- 同一檔案內避免混用 ESM 與 CommonJS。
- 匯入順序：Node 內建模組 -> 第三方套件 -> 專案內部模組。

## 命名規則
- 變數與函式使用 `camelCase`。
- 類別與建構式使用 `PascalCase`。
- 常數使用 `UPPER_SNAKE_CASE`。
- 布林值命名採可判斷語意：`isReady`、`hasError`、`canSubmit`。

## 宣告與型別風格
- 一律使用 `const`，只有確定會重新賦值時才使用 `let`。
- 禁止使用 `var`。
- 優先明確型別語意：避免同一變數承載不同型別。

## 函式與結構
- 單一函式只做一件事，維持短小且可測試。
- 函式參數超過 3 個時，改用物件參數。
- 優先使用早回傳（guard clauses）減少巢狀層級。
- 避免過深巢狀（超過 3 層時應重構）。

## 非同步與錯誤處理
- 優先使用 `async/await`，避免不必要的 Promise 鏈。
- 必要時使用 `try/catch`，錯誤訊息需包含可追查資訊。
- 不可吞錯（empty catch）；至少記錄或重新拋出語意化錯誤。

## 集合與不可變風格
- 優先使用 `map`、`filter`、`reduce` 等陣列方法，不手寫可替代的迴圈。
- 避免直接修改輸入參數（no mutation），必要時建立新物件或新陣列。
- 使用解構與展開運算子提升可讀性，但避免過度巢狀解構。

## 比較與條件
- 一律使用嚴格比較：`===` 與 `!==`。
- 禁止依賴隱性型別轉換；轉型需明確（`Number()`、`String()`）。
- 複雜條件應命名為中介變數，避免長條件直接寫在 `if`。

## 註解與文件
- 註解只說明「為什麼」，不重述「做了什麼」。
- 對外 API 或複雜函式建議補上 JSDoc。
- 移除失效註解與被註解掉的舊程式碼。

## 安全與品質
- 禁止硬編碼敏感資訊（token、密碼、金鑰）。
- 禁止使用 `eval` 與 `new Function`。
- 使用者輸入需先驗證再使用。

## 輸出格式
- 產生或修改 JavaScript 時，遵循以上規範。
- 若有例外需求，需在回覆中說明偏離規範原因。
