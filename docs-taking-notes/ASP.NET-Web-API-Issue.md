# **ASP.NET Web API Issue**

## 1. Issue **"One or more validation errors occurred."**

> { "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1", "title": "One or more validation errors occurred.", "status": 400, "traceId": "00-d0b4a52ee7410b3113c44d6e4d854f04-c4f620b8f232b8bc-00", "errors": { "Role": [ "The Role field is required." ], "Token": [ "The Token field is required." ] } }

![image-20231213010217648](C:\Users\NamVu\AppData\Roaming\Typora\typora-user-images\image-20231213010217648.png)

- Đọc bài này: [c# - ASP.NET Core 6.0 Web API error status: 400 title: "One or more validation errors occurred." for POST Request due to Foreign Keys - Stack Overflow](https://stackoverflow.com/questions/72980204/asp-net-core-6-0-web-api-error-status-400-title-one-or-more-validation-errors)
- since you are using net6 ALL NOT REQUIRED properties should be nullable explicitly. So you should try this
- .net 6 trở lên thì các field/properties trong class nên thêm nullable. 
- Ví dụ: public **string?** testProp {get; set;}