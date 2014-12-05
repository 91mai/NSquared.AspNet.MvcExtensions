#language: zh-TW
功能: 使用ActionFilter提供JsonSchema
	為了方便在開發時期，可以提供回傳資料的JsonSchema
	提供給前端開發人員 （F2E、App）產生其他語言的Class
	透過Asp.Net Mvc Action Filter的方式，自動讀取回傳對應的Type產生JsonSchema

場景: HttpRequest包含Header，並且有指定回傳物件的Type，成功回傳JsonSchema
	假設 HttpRequest的Header包含 "NSquared-Request-JsonSchema", 值為 "True"
	並且 指定物件回傳的Type為 "NSquared.MvcExtensions.Test.Models.TestData"
	當 執行Action時
	那麼 回傳內容為"{"title":"NSquared.MvcExtensions.Test.Models.TestData","type":"object","properties":{"Id":{"required":true,"type":"integer"},"Name":{"required":true,"type":["string","null"]},"Birthday":{"required":true,"type":"string"}}}"

場景: HttpRequest包含Header，但值為False，沒有覆寫回傳內容
	假設 HttpRequest的Header包含 "NSquared-Request-JsonSchema", 值為 "False"
	並且 指定物件回傳的Type為 "NSquared.MvcExtensions.Test.Models.TestData"
	當 執行Action時
	那麼 沒有回傳內容

場景: HttpRequest沒有包含Header，沒有覆寫回傳內容
	並且 指定物件回傳的Type為 "NSquared.MvcExtensions.Test.Models.TestData"
	當 執行Action時
	那麼 沒有回傳內容

場景: HttpRequest包含Header，預設路徑沒有資料，收到錯誤訊息
	假設 HttpRequest的Header包含 "NSquared-Request-JsonSchema", 值為 "True"
	但是 沒有指定回傳物件的Type
	當 執行Action時
	那麼 拋出錯誤訊息
