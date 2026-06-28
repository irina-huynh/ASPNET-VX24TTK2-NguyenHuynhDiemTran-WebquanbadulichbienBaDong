USE master
GO

IF(DB_ID('BaDongTourDb') is not null)
BEGIN
    ALTER DATABASE BaDongTourDb
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE    
    DROP DATABASE BaDongTourDb
END
GO

CREATE DATABASE BaDongTourDb;
GO

USE BaDongTourDb;
GO 
-----------------//---------------


CREATE TABLE Users
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Role NVARCHAR(20) NOT NULL,
    Phone NVARCHAR(20),
    IsLocked BIT NOT NULL DEFAULT 0,
    Avatar NVARCHAR(300),
    CreatedDate DATETIME DEFAULT GETDATE()
)
GO

CREATE TABLE Destinations (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(150) NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    Address NVARCHAR(300),
    Description NVARCHAR(1000),
    Price DECIMAL(18,2),
    ImageUrl NVARCHAR(300),
    GoogleMapUrl NVARCHAR(MAX),
    OpenTime NVARCHAR(50),
    CloseTime NVARCHAR(50)
)

CREATE TABLE TravelCompanies (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CompanyName NVARCHAR(200),
    Phone NVARCHAR(50),
    Email NVARCHAR(100),
    Website NVARCHAR(200),
    Address NVARCHAR(300)
)

CREATE TABLE TourSchedules (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DestinationId INT NULL,
    TravelCompanyId INT NULL,
    Title NVARCHAR(200),
    DayNumber INT,
    TimeText NVARCHAR(100),
    Description NVARCHAR(MAX),

    CONSTRAINT FK_TourSchedules_Destinations 
    FOREIGN KEY (DestinationId) REFERENCES Destinations(Id),

    CONSTRAINT FK_TourSchedules_TravelCompanies 
    FOREIGN KEY (TravelCompanyId) REFERENCES TravelCompanies(Id)
)

CREATE TABLE CustomerReviews
(    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    DestinationId INT NOT NULL,
    Rating INT CHECK(Rating BETWEEN 1 AND 5),
    Comment NVARCHAR(1000),
    ImageUrl NVARCHAR(300),
    CreatedAt DATETIME DEFAULT GETDATE(),

    CONSTRAINT FK_Reviews_User
        FOREIGN KEY(UserId)
        REFERENCES Users(Id),

    CONSTRAINT FK_Reviews_Destination
        FOREIGN KEY(DestinationId)
        REFERENCES Destinations(Id)
)
GO

CREATE TABLE TourCosts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CostName NVARCHAR(200),
    Amount DECIMAL(18,2),
    Description NVARCHAR(500)
)



CREATE TABLE ContactMessages (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100),
    Email NVARCHAR(100),
    Subject NVARCHAR(200),
    Message NVARCHAR(MAX),
    CreatedDate DATETIME DEFAULT GETDATE()
)
GO


INSERT INTO Users
(    FullName,    Username,    Password,    Role,    Phone,    IsLocked,    Avatar)
VALUES
(N'Quản trị viên','admin','123456','Admin','0900000000',0,'/Content/Uploads/default-user.png'),
(N'Nguyễn Huỳnh Diễm Trân','tran','123456','User','0975066447',0,'/Content/Uploads/user1.jpg'),
(N'Trần Thị Mai','mai','123456','User','0902222222',0,'/Content/Uploads/user2.jpg'),
(N'Lê Hoàng Nam','nam','123456','User','0903333333',0,'/Content/Uploads/user3.jpg');


INSERT INTO Destinations
(Name, Type, Address, Description, Price, ImageUrl, GoogleMapUrl, OpenTime, CloseTime)
VALUES

-- BIỂN

(N'Bãi biển Ba Động',
 N'Bãi biển',
 N'Xã Trường Long Hòa, thị xã Duyên Hải, Trà Vinh',
 N'Bãi biển nổi tiếng của tỉnh Trà Vinh với hàng dương xanh mát, không gian yên bình, thích hợp tắm biển, ngắm bình minh và cắm trại.',
 0,
 N'/Content/Uploads/biendong.jpg',
 N'<iframe src="https://www.google.com/maps/embed?pb=!1m16!1m12!1m3!1d15734.519634141483!2d106.54946257466673!3d9.6270992357511!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!2m1!1zYmnhu4NuIGJhIMSR4buZbmc!5e0!3m2!1svi!2s!4v1782621367299!5m2!1svi!2s" width="600" height="450" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="strict-origin-when-cross-origin"></iframe>',
 N'05:00', N'22:00'),

(N'Khu cắm trại ven biển',
 N'Bãi biển',
 N'Gần khu vực Biển Ba Động',
 N'Địa điểm phù hợp để cắm trại qua đêm, tổ chức picnic, sinh hoạt nhóm và ngắm biển.',
 0,
 N'/Content/Uploads/camtrai.jpg',
 N'<iframe src="https://www.google.com/maps/embed?pb=!1m16!1m12!1m3!1d15734.519634141483!2d106.54946257466673!3d9.6270992357511!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!2m1!1zYmnhu4NuIGJhIMSR4buZbmc!5e0!3m2!1svi!2s!4v1782621367299!5m2!1svi!2s" width="600" height="450" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="strict-origin-when-cross-origin"></iframe>',
 N'00:00', N'23:59'),

-- ĐIỂM THAM QUAN

(N'Hàng dương Ba Động',
 N'Điểm tham quan',
 N'Khu du lịch Biển Ba Động',
 N'Hàng dương ven biển tạo nên khung cảnh đặc trưng của Biển Ba Động, thích hợp chụp ảnh và thư giãn.',
 0,
 N'/Content/Uploads/hangduong.jpg',
 N'<iframe src="https://www.google.com/maps/embed?pb=!1m16!1m12!1m3!1d15734.519634141483!2d106.54946257466673!3d9.6270992357511!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!2m1!1zYmnhu4NuIGJhIMSR4buZbmc!5e0!3m2!1svi!2s!4v1782621367299!5m2!1svi!2s" width="600" height="450" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="strict-origin-when-cross-origin"></iframe>',
 N'05:00', N'18:00'),

-- NHÀ HÀNG

(N'Ẩm Thực Sao Biển Ba Động',
 N'Quán ăn',
 N'Khu vực Biển Ba Động',
 N'Phục vụ các món hải sản tươi sống như tôm, cua, ghẹ, mực và nhiều món đặc sản địa phương.',
 200000,
 N'/Content/Uploads/nhahang.jpg',
 N'<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d7867.563121917623!2d106.54852783990475!3d9.614068085415441!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x319f87002e9db815%3A0xe0ad2e7facc68a42!2z4bqobSBUaOG7sWMgU2FvIEJp4buDbiBCYSDEkOG7mW5n!5e0!3m2!1svi!2s!4v1782621438428!5m2!1svi!2s" width="600" height="450" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="strict-origin-when-cross-origin"></iframe>',
 N'08:00', N'22:00'),

(N'Quán ăn Tuấn An',
 N'Quán ăn',
 N'Khu vực biển Ba Động',
 N'Quán ăn địa phương với giá cả bình dân, phù hợp khách du lịch gia đình.',
 150000,
 N'/Content/Uploads/quanhaisan.jpg',
 N'<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3307.9189838302077!2d106.55121374797!3d9.612414404795304!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x319f870015fdfbb5%3A0xbda141ed64d9a616!2zUXXDoW4gxINuIFR14bqlbiBBbg!5e0!3m2!1svi!2s!4v1782621533058!5m2!1svi!2s" width="600" height="450" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="strict-origin-when-cross-origin"></iframe>',
 N'08:00', N'22:00'),

-- KHÁCH SẠN

(N'THE ROSE HOTEL',
 N'Khách sạn',
 N'108 Đ. Nguyễn Hữu Cảnh, Khu vực Long, Thốt Nốt, Cần Thơ, Việt Nam cách biển Ba động 30km',
 N'Khách sạn nghỉ dưỡng gần biển, thuận tiện cho khách lưu trú qua đêm.',
 450000,
 N'/Content/Uploads/khachsan.jpg',
 N'<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3929.8527285380164!2d106.33516298330177!3d9.94620894859557!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31a011695ba78d3f%3A0xa6768aa5ce043722!2sTHE%20ROSE%20HOTEL!5e0!3m2!1svi!2s!4v1782621093776!5m2!1svi!2s" width="600" height="450" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="strict-origin-when-cross-origin"></iframe>',
 N'00:00', N'23:59'),

(N'Malis Homestay',
 N'Homestay',
 N'409 Phạm Ngũ Lão, Trà Vinh, Vĩnh Long, Việt Nam cách biển ba động 30km',
 N'Nhà nghỉ giá rẻ dành cho khách du lịch ngắn ngày.',
 250000,
 N'/Content/Uploads/nhanghi.jpg',
 N'<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2778.8253084753933!2d106.34000538578287!3d9.946233045185805!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31a011515602f4e1%3A0x700c6e5c99458748!2sMalis%20Homestay!5e0!3m2!1svi!2s!4v1782621050807!5m2!1svi!2s" width="600" height="450" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="strict-origin-when-cross-origin"></iframe>',
 N'00:00', N'23:59'),

-- LỄ HỘI

(N'Lễ hội Ok Om Bok',
 N'Lễ hội',
 N'Trà Vinh',
 N'Lễ hội truyền thống của đồng bào Khmer diễn ra vào khoảng tháng 10 âm lịch hằng năm với nhiều hoạt động văn hóa đặc sắc.',
 0,
 N'/Content/Uploads/okombok.jpg',
 N'<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d31441.110463907065!2d106.32253589490811!3d9.922395645361018!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31a01763da2ad3e1%3A0x81d307bbeecf1daa!2zVHLDoCBWaW5oLCBWxKluaCBMb25nLCBWaeG7h3QgTmFt!5e0!3m2!1svi!2s!4v1782621678266!5m2!1svi!2s" width="600" height="450" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="strict-origin-when-cross-origin"></iframe>',
 N'Tháng 10 âm lịch', N'Tháng 10 âm lịch'),

(N'Tết Chol Chnam Thmay',
 N'Lễ hội',
 N'Trà Vinh',
 N'Tết cổ truyền của đồng bào Khmer thường diễn ra vào tháng 4 hằng năm.',
 0,
 N'/Content/Uploads/cholchnamthmay.jpg',
 N'<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d31441.110463907065!2d106.32253589490811!3d9.922395645361018!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31a01763da2ad3e1%3A0x81d307bbeecf1daa!2zVHLDoCBWaW5oLCBWxKluaCBMb25nLCBWaeG7h3QgTmFt!5e0!3m2!1svi!2s!4v1782621678266!5m2!1svi!2s" width="600" height="450" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="strict-origin-when-cross-origin"></iframe>',
 N'Tháng 4', N'Tháng 4'),

(N'Lễ hội Sene Dolta',
 N'Lễ hội',
 N'Trà Vinh',
 N'Một trong những lễ hội lớn của người Khmer Nam Bộ, mang đậm bản sắc văn hóa dân tộc.',
 0,
 N'/Content/Uploads/senedolta.jpg',
 N'<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d31441.110463907065!2d106.32253589490811!3d9.922395645361018!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31a01763da2ad3e1%3A0x81d307bbeecf1daa!2zVHLDoCBWaW5oLCBWxKluaCBMb25nLCBWaeG7h3QgTmFt!5e0!3m2!1svi!2s!4v1782621678266!5m2!1svi!2s" width="600" height="450" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="strict-origin-when-cross-origin"></iframe>',
 N'Tháng 9 âm lịch', N'Tháng 9 âm lịch');
GO

INSERT INTO TravelCompanies
(CompanyName, Phone, Email, Website, Address)
VALUES
(N'Mekong', N'0938 701 737', N'contact.mekonginternational@gmail.com', N'https://mekonginternational.vn/du-lich/', N'Trà Vinh'),
(N'Ba Động Local Tour', N'+84 28 3861 4699', N'cs@traveloka.com', N'https://www.traveloka.com/vi-vn/explore/destination/bien-ba-dong/301507', N'Duyên Hải, Trà Vinh'),
(N'Tiền Giang Tourist', N'(0273) 3886941', N'contact@tiengiangtourist.vn', N'https://tiengiangtourist.vn/tour-du-lich-tra-vinh/', N'63 Trưng Trắc, Phường 1, TP Mỹ Tho, Tiền Giang')
GO

INSERT INTO TourSchedules
(DestinationId, TravelCompanyId, Title, DayNumber, TimeText, Description)
VALUES
(1, 2, N'Gợi ý tham quan Biển Ba Động 1 ngày', 1, N'05:30 - 06:30',
 N'Ngắm bình minh trên biển, dạo biển và chụp ảnh tại bãi biển Ba Động.'),

(1, 2, N'Gợi ý tham quan Biển Ba Động 1 ngày', 1, N'07:00 - 09:00',
 N'Tắm biển, vui chơi nhẹ và thư giãn tại bãi biển. Hoạt động này thường miễn phí.'),

(3, 2, N'Gợi ý tham quan Biển Ba Động 1 ngày', 1, N'09:00 - 10:30',
 N'Tham quan hàng dương ven biển, khu vực check-in và tìm hiểu đời sống người dân địa phương.'),

(4, 2, N'Gợi ý tham quan Biển Ba Động 1 ngày', 1, N'11:00 - 13:00',
 N'Ăn trưa tại các quán hải sản hoặc nhà hàng lân cận khu vực Biển Ba Động.'),

(1, 1, N'Gợi ý tham quan 2 ngày 1 đêm', 1, N'Ngày 1 - Chiều',
 N'Đến Biển Ba Động, nhận phòng tại khách sạn hoặc nhà nghỉ lân cận, sau đó tắm biển và dạo biển.'),

(4, 1, N'Gợi ý tham quan 2 ngày 1 đêm', 1, N'Ngày 1 - Tối',
 N'Ăn hải sản tại khu vực gần biển, có thể cắm trại tự túc nếu đi theo nhóm.'),

(3, 1, N'Gợi ý tham quan 2 ngày 1 đêm', 2, N'Ngày 2 - Sáng',
 N'Ngắm bình minh, chụp ảnh tại hàng dương ven biển và tham quan chợ hải sản địa phương.'),

(8, 3, N'Lịch trình tham gia lễ hội Ok Om Bok', 1, N'Tháng 10 âm lịch',
 N'Du khách có thể kết hợp tham quan Biển Ba Động và tham gia lễ hội Ok Om Bok của đồng bào Khmer tại Trà Vinh.'),

(9, 3, N'Lịch trình tham gia Tết Chol Chnam Thmay', 1, N'Tháng 4 hằng năm',
 N'Gợi ý du khách đến Trà Vinh vào dịp Tết Chol Chnam Thmay để tìm hiểu văn hóa Khmer, sau đó kết hợp tham quan Biển Ba Động.'),

(1, 2, N'Lịch trình mùa hè tại Biển Ba Động', 1, N'Tháng 5 - 8 hằng năm',
 N'Đây là thời điểm phù hợp để đi biển, tắm biển, cắm trại, vui chơi nhóm và thưởng thức hải sản địa phương.')
GO

INSERT INTO TourCosts
(CostName, Amount, Description)
VALUES
(N'Di chuyển', 200000, N'Tùy điểm xuất phát và phương tiện.'),
(N'Ăn uống hải sản', 250000, N'Tùy thực đơn và số lượng khách.'),
(N'Khách sạn', 450000, N'Áp dụng nếu ở lại qua đêm.'),
(N'Điểm vui chơi liên quan', 50000, N'Chi phí dự kiến cho các dịch vụ phụ.')
GO


INSERT INTO CustomerReviews
(UserId,DestinationId,Rating,Comment,ImageUrl)
VALUES
(2,1,5,
N'Biển rất đẹp, nước sạch, không khí mát mẻ.',
'/Content/Uploads/biendong.jpg'),

(3,4,4,
N'Hải sản ngon và giá hợp lý.',
'/Content/Uploads/nhahang.jpg'),

(4,8,5,
N'Lễ hội Ok Om Bok rất đông vui.',
'/Content/Uploads/okombok.jpg'),

(3,1,4,
N'Địa điểm thích hợp cho gia đình cuối tuần.',
'/Content/Uploads/camtrai.jpg');
GO

