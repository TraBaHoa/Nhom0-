-- Lệnh này tự động tìm ID của tài khoản mới và gán vào nhóm Admin
INSERT INTO [AspNetUserRoles] (UserId, RoleId)
SELECT 
    (SELECT TOP 1 Id FROM AspNetUsers WHERE Email = 'travinh572005@gmail.com'),
    (SELECT TOP 1 Id FROM AspNetRoles WHERE Name = 'Admin')

SELECT * FROM AspNetUserRoles