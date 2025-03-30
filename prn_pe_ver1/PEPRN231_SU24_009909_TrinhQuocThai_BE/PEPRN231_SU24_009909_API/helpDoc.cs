 Bước 1: Tải package ở Repository

dotnet add package Microsoft.EntityFrameworkCore --version 8.0.5
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.5
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.5
dotnet add package Microsoft.Extensions.Configuration --version 8.0.0
dotnet add package Microsoft.Extensions.Configuration.Json --version 8.0.0

hoặc copy này rồi build
FE - MVC
  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.11">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="8.0.7" />
    <PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
  </ItemGroup>
BE - API

 <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.1" />
    <PackageReference Include="Microsoft.AspNetCore.OData" Version="8.2.5" />
  </ItemGroup>
Repo

 <ItemGroup>
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.5" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.5" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.5">
  <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
  <PrivateAssets>all</PrivateAssets>
</PackageReference>
<PackageReference Include="Microsoft.Extensions.Configuration" Version="8.0.0" />
<PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="8.0.0" />
  </ItemGroup>

Bước 2: tạo DB

Bước 3: thêm model vào Repo (dùng EF core power tool)

chuột phải vô repo chọn ef core power tool
phải thêm connection string vô file context
    public static string? GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string? connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection"));

appsetting

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=MAY_CAY\\MSSQLSERVER01;Initial Catalog=EnglishPremierLeague2024DB;Persist Security Info=True;User ID=sa;Password=123456;Encrypt=False"
  },

  "Jwt": {
    "Key": "0ccfeb299b126a479a64630e2d34e9e91e5fcbcaea8ac9e3347e224b0557a53e",
    "Issuer": "http://localhost:5222",
    "Audience": "http://localhost:5222"
  }
}


bước 4: thêm base repo
vô code mà lấy

Bước 5: thêm dependences

Bước 6: Code trên service 

- tạo class service login trước
   + add code trong file service 
   + add scope trong program fie (them du cac code luo.......)
   + thêm controller login

- tạo class service bang chính   Get -> GetById -> Delete -> Create -> Update (hai thg cuoi phai validate)
   + add code trong file service 
   + add scope 
   + tao file controller 
   + check authen autho role 

bước 7 test (test trc khi làm FE)

bước 8 tạo projet fe 

bước 9 thêm connectionstring vô 
- file context 
    public static string? GetConnectionString(string connectionStringName)
 {
     var config = new ConfigurationBuilder()
         .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
         .AddJsonFile("appsettings.json")
         .Build();

     string? connectionString = config.GetConnectionString(connectionStringName);
     return connectionString;
 }

 protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection"));

- file program
builder.Services.AddDbContext<EnglishPremierLeague2024DBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

- file appseting 
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=MAY_CAY\\MSSQLSERVER01;Initial Catalog=EnglishPremierLeague2024DB;Persist Security Info=True;User ID=sa;Password=123456;Encrypt=False"
  },


buoc 10 generate controller (comment ham CUD lam get trc)
buoc 11 xoa toan bo code o buoc 9
buoc 12 them VM + loginReq cho ham get va detail va login

buoc 13 add cookie tron file program
buoc 14 tao controller login
buoc 15 chinh view login (using logreq model)
