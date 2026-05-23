# InBody User Story

## 背景
使用者可輸入身體基本資訊，系統需依據輸入完整度，動態控制 BMI、BMR、TDEE 三個功能按鈕是否可點擊。

## User Story 1：輸入身體基本資訊
**As a** 使用者  
**I want** 輸入我的身體基本資訊  
**So that** 系統可以判斷我可進行哪些身體指標計算（BMI / BMR / TDEE）

### 驗收條件
1. 畫面提供身體基本資訊輸入欄位（例如：身高、體重、年齡、性別、活動量）。
2. 使用者可修改欄位內容，系統需即時更新按鈕狀態。

## User Story 2：BMI 按鈕啟用條件
**As a** 使用者  
**I want** 在輸入 BMI 計算所需資訊後使用 BMI 功能  
**So that** 我可以計算我的身體質量指數

### 驗收條件
1. 當 BMI 必要資訊皆已輸入完成時，`BMI` 按鈕為 Enabled。
2. 當 BMI 必要資訊任一未完成時，`BMI` 按鈕為 Disabled。
3. 系統需依輸入狀態即時切換 `BMI` 按鈕 Enabled/Disabled。

## User Story 3：BMR 按鈕啟用條件
**As a** 使用者  
**I want** 在輸入 BMR 計算所需資訊後使用 BMR 功能  
**So that** 我可以計算我的基礎代謝率

### 驗收條件
1. 當 BMR 必要資訊皆已輸入完成時，`BMR` 按鈕為 Enabled。
2. 當 BMR 必要資訊任一未完成時，`BMR` 按鈕為 Disabled。
3. 系統需依輸入狀態即時切換 `BMR` 按鈕 Enabled/Disabled。

## User Story 4：TDEE 按鈕啟用條件
**As a** 使用者  
**I want** 在輸入 TDEE 計算所需資訊後使用 TDEE 功能  
**So that** 我可以計算我的每日總消耗熱量

### 驗收條件
1. 當 TDEE 必要資訊皆已輸入完成時，`TDEE` 按鈕為 Enabled。
2. 當 TDEE 必要資訊任一未完成時，`TDEE` 按鈕為 Disabled。
3. 系統需依輸入狀態即時切換 `TDEE` 按鈕 Enabled/Disabled。

## 規則補充
1. 三個按鈕（BMI / BMR / TDEE）各自依據自己的必要欄位判斷是否 Enabled，不互相影響。
2. 只要必填欄位由完整變為不完整，對應按鈕需立即改為 Disabled。
3. 按鈕 Disabled 時不可觸發計算行為。
