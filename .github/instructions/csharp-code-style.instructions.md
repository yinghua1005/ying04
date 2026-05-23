---
description: "Use when writing or refactoring C# files. Enforces C# coding style, naming, async patterns, exception handling, and maintainable architecture practices."
name: "C# Code Style"
applyTo:
  - "**/*.cs"
  - "**/*.csx"
---
# C# 程式碼規範

## 目標
- 提升可讀性、一致性與可維護性。
- 優先可測試、可除錯、可擴充的實作方式。
- 本規範以專案自訂規則為準，必要時可依情境調整。

## 命名規則
- 類別、介面、列舉、公開成員使用 `PascalCase`。
- 區域變數與參數使用 `camelCase`。
- 私有欄位使用 `_camelCase`。
- 介面命名以 `I` 開頭（例如 `IUserService`）。
- 非同步方法名稱需以 `Async` 結尾。

## 宣告與型別風格
- 優先使用 `var` 於右值型別明確情境；型別不明確時使用顯式型別。
- 優先使用 `const`（編譯期常數）與 `readonly`（不可變欄位）。
- 建議啟用 Nullable Reference Types，並在可為 null 情境使用 nullable 註記（`?`）。
- 禁止使用 `dynamic`，除非有明確互通需求且附註原因。

## 類別與方法設計
- 一個類別只負責單一職責，避免過度肥大類別。
- 方法保持短小，參數過多時改用物件封裝。
- 優先使用早回傳（guard clauses）降低巢狀層級。
- 對外公開 API 需維持穩定命名與行為。

## 例外與錯誤處理
- 只捕捉可處理的例外，避免攔截過度廣泛例外。
- 禁止空的 `catch` 區塊，不可吞錯。
- 重新拋出時保留原始堆疊（使用 `throw;`）。
- 錯誤訊息需包含可追查上下文（識別碼、關鍵參數）。

## 非同步與並行
- I/O 密集流程優先使用 `async/await`。
- 避免 `Task.Result` 或 `Task.Wait()` 造成阻塞或死鎖。
- 可取消的長任務需支援 `CancellationToken`。
- 在函式庫程式碼可使用 `ConfigureAwait(false)`；應用層依專案需求決定。

## 集合與 LINQ
- 讀取轉換優先使用 LINQ，但避免過度鏈式影響可讀性。
- 對效能敏感路徑避免重複列舉與不必要配置。
- 回傳集合優先使用唯讀介面（如 `IReadOnlyList<T>`）表達意圖。

## 可測試性
- 透過介面抽象外部相依，避免硬耦合。
- 業務邏輯避免直接存取靜態全域狀態。
- 時間、隨機、I/O 應可替換或注入以利測試。

## 註解與文件
- 註解說明「為什麼」，避免重述程式碼字面行為。
- 公開類別與方法建議補上 XML 文件註解。
- 移除過期註解與註解掉的舊程式碼。

## 安全與品質
- 禁止硬編碼敏感資訊（密碼、金鑰、連線字串）。
- 外部輸入需做驗證與邊界檢查。
- 禁止為了通過編譯而任意忽略警告。

## 輸出格式
- 產生或修改 C# 程式碼時，遵循以上規範。
- 若有例外需求，需在回覆中說明偏離規範原因。
