USE [WebTinTuc]
GO

-- 1. TẠO ROLE ADMIN (NẾU CHƯA CÓ)
IF NOT EXISTS (SELECT * FROM AspNetRoles WHERE Name = 'Admin')
BEGIN
    INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) 
    VALUES (NEWID(), 'Admin', 'ADMIN', NEWID())
END

-- 2. LẤY ID CỦA USER VÀ ID CỦA ROLE
DECLARE @UserId NVARCHAR(450) = (SELECT Id FROM AspNetUsers WHERE Email = 'travinh572005@gmail.com')
DECLARE @RoleId NVARCHAR(450) = (SELECT Id FROM AspNetRoles WHERE Name = 'Admin')

-- 3. GÁN QUYỀN VÀO BẢNG TRUNG GIAN
IF @UserId IS NOT NULL AND @RoleId IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT * FROM AspNetUserRoles WHERE UserId = @UserId AND RoleId = @RoleId)
    BEGIN
        INSERT INTO AspNetUserRoles (UserId, RoleId) 
        VALUES (@UserId, @RoleId)
        PRINT 'Chúc mừng Vinh! Đã gán quyền Admin thành công cho ' + 'travinh572005@gmail.com'
    END
    ELSE
    BEGIN
        PRINT 'Tài khoản này đã là Admin rồi.'
    END
END
ELSE
BEGIN
    PRINT 'LỖI: Không tìm thấy Email hoặc Role. Hãy kiểm tra lại bảng AspNetUsers!'
END