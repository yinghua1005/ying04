---
name: daily-task-log
description: 'Record today completed tasks into logs/yyyy-MM-dd.md. Create one file per day, append to same file if it already exists, and use fixed session/prompt format.'
argument-hint: '提供 session 名稱與多個 Prompt/Ask-Plan 內容'
user-invocable: true
disable-model-invocation: false
---

# Daily Task Log

將今天完成的任務寫入工作區 `logs/yyyy-MM-dd.md`。

## 何時使用
- 需要記錄今天完成的任務摘要
- 要把多次 Prompt 與 Agent 計畫保留在同一日誌檔
- 希望每天只有一個日誌檔，避免分散

## 輸入資訊
- `session name`
- 多筆 Prompt 與對應的 Ask/Agent/plan（含 Model）

## 程序
1. 使用台灣時區（`Asia/Taipei`）取得今天日期（`yyyy-MM-dd`），組成目標路徑：`logs/yyyy-MM-dd.md`。
2. 檢查 `logs/` 資料夾是否存在；不存在就建立。
3. 檢查當日日誌檔是否存在：
   - 不存在：建立新檔並寫入本次 session 區塊。
   - 已存在：在檔案尾端追加本次 session 區塊（前面保留一個空行做分隔）。
4. 計算 `Prompt[n]`：
   - 若檔案不存在，`n` 從 `1` 開始。
   - 若檔案已存在，先讀取當日檔案中現有最大 `Prompt[n]`，本次從 `max + 1` 連續遞增。
5. 使用以下固定格式輸出，不增刪欄位名稱：

```md
[session name]
### Prompt[1]: [....]
[Ask /Agent/ plan] (Model): [...]
### Prompt[2]: [....]
[Ask /Agent/ plan] (Model): [...]
```

## 實作規則
- 一天只使用一個檔案：`logs/yyyy-MM-dd.md`。
- 追加時不可覆蓋既有內容。
- 每個 session 區塊之間至少保留一個空行。
- 若使用者只提供一筆 Prompt，也必須維持相同格式。
- `Prompt[n]` 採同一天檔案內全域遞增，不在新 session 重置。

## 完成檢查
- 檔案路徑正確：`logs/yyyy-MM-dd.md`
- 當日只有一個日誌檔，且本次內容已寫入
- 內容格式符合模板且 `Prompt[n]` 編號連續
