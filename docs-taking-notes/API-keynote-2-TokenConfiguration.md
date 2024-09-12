# **Token**

1. Tạo hoặc generater 1 secret key trong appsettings.( tạm thời là vậy)![image-20231212002727452](C:\Users\NamVu\AppData\Roaming\Typora\typora-user-images\image-20231212002727452.png)

2. Tạo 1 class AppSettings![image-20231212002852735](C:\Users\NamVu\AppData\Roaming\Typora\typora-user-images\image-20231212002852735.png)

3. Cấu hình nó vào trong Programs (phía dưới AddSwaggerGen())![image-20231212004449607](C:\Users\NamVu\AppData\Roaming\Typora\typora-user-images\image-20231212004449607.png)![image-20231212004655639](C:\Users\NamVu\AppData\Roaming\Typora\typora-user-images\image-20231212004655639.png)

4. Add Authentication Token using jwt with Bearer

   ![image-20231212005011495](C:\Users\NamVu\AppData\Roaming\Typora\typora-user-images\image-20231212005011495.png)

**`dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.0`**

với net.7 thì ta nên thêm --version 7.0.0

Sau khi configure Token cho Authenticate thì add nó vào dưới dạng JwtBearer

```c#
using *Microsoft*.*AspNetCore*.*Authentication*.*JwtBearer*;

builder.Services.AddAuthentication(x => {
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer( y=> {
        y.RequireHttpMetadata = false;
        y.SaveToken = true;
        y.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
		};
    }
);
```

![image-20231212010135874](C:\Users\NamVu\AppData\Roaming\Typora\typora-user-images\image-20231212010135874.png)

===> ![image-20231212011353924](C:\Users\NamVu\AppData\Roaming\Typora\typora-user-images\image-20231212011353924.png)

> Signing keys are used to sign ID tokens, access tokens, SAML assertions, and WS-Fed assertions sent to your application or API. The signing key is a **JSON web key** (JWK) that contains a well-known public key used to validate the signature of a signed JSON web token (JWT).
>
> Dịch: Signing keys được dùng để ký ID tokens, access tokens, SAML assertion(quyền hạn) và WS-Fed assertions được gửi đến ứng dụng hoặc API.
>
> Signing key là 1 khóa JSON web , chứa 1 khóa công khai phổ biến được dùng để thẩm định chữ ký của 1 token JSON web đã ký.

![image-20231212011827391](C:\Users\NamVu\AppData\Roaming\Typora\typora-user-images\image-20231212011827391.png)

- Dòng 48: Lấy cấu hình của AppSettings trong appsettings.json gán vào biến
- Dòng 49: Cấu hình nó vào trong Ứng dụng
- Dòng 51: Lấy giá trị trong AppSettings
- Dòng 52: Chuyển trường Secret từ ASCII to Byte và gán vào key
- Dòng 53: cấu hình xác thực với DefaultAuthenticate: để xác định người dùng đã đăng nhập hay không, và DefaultChallenge: để xác định quyền hạn của người dùng(khi access các tài nguyên cần có quyền)[Authorization]
  - sau đấy thiết lập vào trong JwtBearer với
  - RequireHttpsMetadata  = false: cho phép authenticate qua http
  - SaveToken = true: sau khi xác thực thành công thì sẽ lưu giữ token cho tới khi timeout hoặc người dùng log out ra.
  - TokenValidationParameters gồm
    - ValidateIssuerSigningKey = true:Yêu cầu xác thực chữ ký.
    - IssuerSigningKey = new SymmetricSecurityKey(key): Issuer được gán với key trong AppSettings được dùng để so sánh chữ ký của token
    - ValidateIssuer = false và ValidateAudience = false: mặc định của 2 giá trị là true; 
      - Issuer: Người phát hành token, là từ bên trong Ứng dụng chứ không phải ứng dụng nào khác.
      - Audience: kiểm tra Token của người dùng có quyền để làm hành động nào đó.

> Kết quả là, cấu hình này làm cho middleware xác thực JWT chấp nhận tất cả các token với chữ ký hợp lệ từ bất kỳ nguồn nào mà không kiểm tra Issuer và Audience. Điều này thích hợp trong một số trường hợp, như khi bạn đang phát triển và không quan tâm đến việc kiểm tra Issuer và Audience trong môi trường phát triển. Tuy nhiên, trong môi trường sản xuất, bạn thường muốn thực hiện các kiểm tra này để đảm bảo tính toàn vẹn và an toàn của hệ thống.

5. Sau đấy add app.Authentication() và app.Authorization()
   - phải có authen rồi mới author nếu không sẽ không work.

6. Thêm CORS vào trong Program.cs

   ![image-20231212015934342](C:\Users\NamVu\AppData\Roaming\Typora\typora-user-images\image-20231212015934342.png)

   - CORS: Cross Origin Resource Sharing: [CORS là gì? Giới thiệu tất tần tật về CORS | TopDev](https://topdev.vn/blog/cors-la-gi/)
     - lỗi: ACCESS-CONTROL-ALLOW-ORIGIN IN HEADER: 
       - Dùng để block mã độc khỏi việc đánh cắp thông tin của bạn, 
       - SAME-ORIGIN Policy: để ngăn chặn việc 1 web độc có thể lấy cắp thông tin từ các tab khác trên máy của bạn.
     - CORS: dùng các HTTP headers để notify cho trình duyệt rằng , WebAPI đang chạy ở origin này(domain) có thể truy cập các tài nguyên ở origin khác (WebUI).
       - AllowAnyOrigin(): Cho phép yêu cầu từ bất kỳ nguồn nào, không giới hạn theo domain.
       - AllowAnyMethod(): Cho phép bất kỳ phương thức HTTP nào(CRUD).
       - AllowAnyHeader(): Cho phép sử dụng bất kỳ header HTTP nào.