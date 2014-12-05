#language: zh-TW
功能: 使用ActionFilter提供測試資料
	為了方便在開發時期，可以在開發完成前先提供測試用的Api
	提供給前端開發人員 （F2E、App）
	透過Asp.Net Mvc Action Filter的方式，自動讀取回傳對應的Json檔案並回傳

場景: HttpRequest包含Header，並且有測試資料，成功回傳測試資料
	假設 HttpRequest的Header包含 "NSquared-Request-Dummy", 值為 "True"
	並且 測試Json存在路徑 "~/Dummy/Home/Dummy.json"，內容為 "{ "id" : 1 }"
	當 執行Controller: "Home", Action: "Dummy" 時
	那麼 回傳內容為"{ "id" : 1 }"

場景: HttpRequest包含Header，但值為False，沒有覆寫回傳內容
	假設 HttpRequest的Header包含 "NSquared-Request-Dummy", 值為 "False"
	並且 測試Json存在路徑 "~/Dummy/Home/Dummy.json"，內容為 "{ "id" : 1 }"
	當 執行Controller: "Home", Action: "Dummy" 時
	那麼 沒有回傳內容

場景: HttpRequest沒有包含Header，沒有覆寫回傳內容
	假設 測試Json存在路徑 "~/Dummy/Home/Dummy.json"，內容為 "{ "id" : 1 }"
	當 執行Controller: "Home", Action: "Dummy" 時
	那麼 沒有回傳內容

場景: HttpRequest包含Header，預設路徑沒有資料，收到錯誤訊息
	假設 HttpRequest的Header包含 "NSquared-Request-Dummy", 值為 "True"
	並且 沒有測試用的Json
	當 執行Controller: "Home", Action: "Dummy" 時
	那麼 拋出錯誤訊息