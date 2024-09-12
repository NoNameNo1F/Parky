# **Các bước để thêm 1 Model -> Repository =>**

1. Tạo Model cần thêm vào![image-20231211231235823](C:\Users\NamVu\AppData\Roaming\Typora\typora-user-images\image-20231211231235823.png)

2. Tạo DbSet trong Data/ApplicationDbContext![image-20231211231319690](C:\Users\NamVu\AppData\Roaming\Typora\typora-user-images\image-20231211231319690.png)

3. Migration Model tới DB

   a. dotnet ef migrations add {NoiDungThucHien}

   b. dotnet ef database update

   c. dotnet ef migrations list

![image-20231211231608204](C:\Users\NamVu\AppData\Roaming\Typora\typora-user-images\image-20231211231608204.png)

4. Tạo Interface cho Model và tạo class Model kế thừa từ Interface ![image-20231211231920782](C:\Users\NamVu\AppData\Roaming\Typora\typora-user-images\image-20231211231920782.png)
   - Lớp Repository chứ các method cho model để có thể hoạt đô

![image-20231211231938645](C:\Users\NamVu\AppData\Roaming\Typora\typora-user-images\image-20231211231938645.png)

5. AddScope vào trong Program.cs![image-20231211232041052](C:\Users\NamVu\AppData\Roaming\Typora\typora-user-images\image-20231211232041052.png)

6. public User Authenticate(string username, string password)

![image-20231212174605801](C:\Users\NamVu\AppData\Roaming\Typora\typora-user-images\image-20231212174605801.png)

- Tạo JwtToken

- Convert chuỗi secret thành byte gán vào key

-  Tạo token chi tiết: với Subject = quyền của token's user, 

  ​					Expires = Set timeout

  ​					SigningCredentials = mã hóa key bằng hmac256

-  tạo Token với các header chi tiết trong tokenDescriptor

- gán token vào trong user.Token.