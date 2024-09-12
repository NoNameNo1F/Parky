# **HttpClient , IHttpClientFactory**

# **JWT & Claim Identity Authentication with Cookie**

[c# - ASP .NET CORE 2.2 JWT & Claims identity Authentication for Website - Stack Overflow](https://stackoverflow.com/questions/55628357/asp-net-core-2-2-jwt-claims-identity-authentication-for-website)

in Program.cs add the configuration below

1. builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

   ![image-20231213172627552](C:\Users\NamVu\AppData\Roaming\Typora\typora-user-images\image-20231213172627552.png)

In the Configuration method {CONTROLLER}

![image-20231213174207421](C:\Users\NamVu\AppData\Roaming\Typora\typora-user-images\image-20231213174207421.png)

Sau khi đăng nhập, thì lúc này user đã có được token.

- ClaimsIdentity là instance để chứa thông tin của người dùng(tên, vai trò,...). Được dùng để phân quyền trong ứng dụng.

- ClaimsPrincipal: là instance cao hơn ClaimsIdentity nhưng phân quyền của toàn bộ người dùng trong ứng dụng.

