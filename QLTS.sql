--

--DROP TABLE IF EXISTS delivery;
--DROP TABLE IF EXISTS order_details;
--DROP TABLE IF EXISTS orders;
--DROP TABLE IF EXISTS users;
--DROP TABLE IF EXISTS categories;
--DROP TABLE IF EXISTS products;
--DROP TABLE IF EXISTS customers
--Create database QLTraSua
use  QLTraSua

CREATE TABLE categories (
    category_id INT IDENTITY(1,1) PRIMARY KEY,  
    category_name NVARCHAR(255)
);

-- Create Users Table
CREATE TABLE users (
    user_id INT IDENTITY(1,1) PRIMARY KEY, 
    first_name NVARCHAR(30),
    last_name NVARCHAR(30),
    username VARCHAR(50),
    email VARCHAR(100),
    password VARCHAR(60),
    phone VARCHAR(20)
);

ALTER TABLE users
ADD role VARCHAR(10);  -- cột role lưu giá trị là 'USER' hoặc 'ADMIN'

ALTER TABLE users
ADD CONSTRAINT chk_role CHECK (role IN ('USER', 'ADMIN'));

-- Create Orders Table
CREATE TABLE orders (
    order_id INT IDENTITY(1,1) PRIMARY KEY,  
    order_date DATE,
    total_amount DECIMAL(10,2),
    status BIT,
    user_id INT FOREIGN KEY REFERENCES users(user_id)
);


-- Create Products Table
CREATE TABLE products (
    product_id INT IDENTITY(1,1) PRIMARY KEY, 
    product_name NVARCHAR(255),
    price DECIMAL(10, 2),
    category_id INT FOREIGN KEY REFERENCES categories(category_id) 
        ON DELETE CASCADE ON UPDATE CASCADE, 
    image_url VARCHAR(255)
);

-- Create Order Details Table
CREATE TABLE order_details (
    order_id INT,
    product_id INT,
    quantity INT,
    temperature NVARCHAR(5),
    size VARCHAR(1),
    sugar NVARCHAR(4),
    ice NVARCHAR(4),
    PRIMARY KEY (order_id, product_id),
    FOREIGN KEY (order_id) REFERENCES orders(order_id) 
        ON DELETE CASCADE ON UPDATE CASCADE, 
    FOREIGN KEY (product_id) REFERENCES products(product_id) 
        ON DELETE CASCADE ON UPDATE CASCADE  
);

-- Create Delivery Table
CREATE TABLE delivery (
    delivery_id INT IDENTITY(1,1) PRIMARY KEY,
    customer_name NVARCHAR(255),
    customer_phone VARCHAR(20),
    customer_location NVARCHAR(255),
    customer_note NVARCHAR(255) NULL,

    order_id INT FOREIGN KEY REFERENCES orders(order_id)
        ON DELETE CASCADE ON UPDATE CASCADE
);

-- Insert sample data for Categories
INSERT INTO categories(category_name)
VALUES 
    (N'Trà sữa'), 
    (N'Fresh Fruit Tea'), 
    (N'Macchiato Cream Cheese'), 
    (N'Cà Phê'), 
    (N'Ice Cream'), 
    (N'Special Menu'), 
    (N'Món nổi bật'); 


	
-- chèn dữ liệu vào bảng sản phẩm
-- chèn dữ liệu danh mục Trà sữa - category_id 1
insert into products(product_name,price, category_id,image_url) values (N'Ô Long Đào Lê Tây Bắc', '25.000', 1, 'https://tocotocotea.com/wp-content/uploads/2024/08/O-Long-Dao-Le-Tay-Bac.png');
insert into products(product_name,price, category_id,image_url) values (N'Xanh Nhài Lê Tây Bắc', '25.000', 1, 'https://tocotocotea.com/wp-content/uploads/2024/08/Xanh-Nhai-Le-Tay-Bac.png');
insert into products(product_name,price, category_id,image_url) values (N'Trà sữa Boba Cheese', '25.000', 1, 'https://tocotocotea.com/wp-content/uploads/2024/08/Tra-Sua-BoBa-Cheese.png');
insert into products(product_name,price, category_id,image_url) values (N'Ô Long Sữa Trân Châu Ngũ Cốc', '25.000', 1, 'https://tocotocotea.com/wp-content/uploads/2023/11/z4925614515113_5bdf67d7e4b3ee98215ea11da9b303e9.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Sữa Tươi Trân Châu Đường Hổ Khổng Lồ', '35.000', 1, 'https://tocotocotea.com/wp-content/uploads/2023/10/z4853951474939_d2f4544ce4706979c9779d466a8efc5e.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Người Bạn Xanh Sữa Nhài Khổng Lồ', '35.000', 1, 'https://tocotocotea.com/wp-content/uploads/2023/09/z4704498631032_4dc66b5cea996f0c14840a46043f161c.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Trà Sữa Trân Châu Đường Hổ', '25.000', 1, 'https://tocotocotea.com/wp-content/uploads/2023/04/tra-sua-tran-chau-duong-den.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Trà Sữa Phô Mai Tươi', '25.000', 1, 'https://tocotocotea.com/wp-content/uploads/2023/01/Tra-sua-pho-mai-tuoi.png');
insert into products(product_name,price, category_id,image_url) values (N'Tiger Sugar', '25.000', 1, 'https://tocotocotea.com/wp-content/uploads/2021/06/TS_TIGER_SUGAR.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Trà Sữa Trân Châu Hoàng Gia', '25.000', 1, 'https://tocotocotea.com/wp-content/uploads/2021/01/Tra-Sua-Tran-Chau-Hoang-Gia-1-copy.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Trà Sữa Ba Anh Em', '25.000', 1,'https://tocotocotea.com/wp-content/uploads/2021/01/ba-anh-em.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Trà Sữa Trân Châu Sợi', '25.000', 1, 'https://tocotocotea.com/wp-content/uploads/2021/01/tra-sua-tran-chau-soi.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Trà Sữa Kim Cương Đen Okinawa', '25.000', 1, 'https://tocotocotea.com/wp-content/uploads/2021/01/Tra-Sua-Okinawa_Moi.png');
insert into products(product_name,price, category_id,image_url) values (N'Trà Xanh Sữa Vị Nhài', '25.000', 1, 'https://tocotocotea.com/wp-content/uploads/2021/01/Tra-Xanh-Sua-Vi-Nhai-1-copy-1.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Trà Sữa Ô Long', '25.000', 1, 'https://tocotocotea.com/wp-content/uploads/2021/01/Tra-O-Long-Sua-2-copy.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Trà Sữa Socola', '25.000', 1, 'https://tocotocotea.com/wp-content/uploads/2021/01/Tra-Sua-Socola-1-copy.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Trà sữa dâu tây', '25.000', 1, 'https://tocotocotea.com/wp-content/uploads/2021/01/Tra-Sua-Dau-Tay-2-copy.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Trà sữa', '25.000', 1, 'https://tocotocotea.com/wp-content/uploads/2021/01/Tra-Sua-1-copy.jpg');

-- chèn dữ liệu danh mục fresh fruit tea - category_id 2
insert into products(product_name,price, category_id,image_url) values (N'Trà Xanh Nhài Đào Tiên', '25.000', 2, 'https://tocotocotea.com/wp-content/uploads/2024/04/Tra-Xanh-Dao-Que-Hoa.png')
insert into products(product_name,price, category_id,image_url) values (N'Trà Đào Tiên Quế Hoa', '25.000', 2, 'https://tocotocotea.com/wp-content/uploads/2024/04/Tra-Dao-Tien-Que-Hoa-1.png')
insert into products(product_name,price, category_id,image_url) values (N'Ô Long Tuyết Lê Khổng Lồ', '35.000', 2, 'https://tocotocotea.com/wp-content/uploads/2024/01/z5200313014176_88115b1d87eae335d18ada8210389e0d.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Trà Chanh Mật Ong Giã Tay Khổng Lồ', '35.000', 2, 'https://tocotocotea.com/wp-content/uploads/2023/12/z4967287161207_d582f10a876bcc7359666df4f2ec76c5.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Trà Chanh Mật Ong Giã Tay', '25.000', 2, 'https://tocotocotea.com/wp-content/uploads/2023/11/z4925614520640_1babe5daea83472f4784ceb3cd206979.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Ô Long Mận Mộc Châu Thạch Quế Hoa', '25.000', 2, 'https://tocotocotea.com/wp-content/uploads/2022/10/tra-man.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Trà dâu tằm pha lê tuyết', '25.000', 2, 'https://tocotocotea.com/wp-content/uploads/2021/01/Tra-Dau-Tam-Pha-Le-Tuyet-2-copy.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Trà Dứa Thạch Konjac', '25.000', 2, 'https://tocotocotea.com/wp-content/uploads/2021/01/tra-dua.jpg');

-- chèn dữ liệu danh mục Macchiato Cream Cheese - category_id 3
insert into products(product_name,price, category_id,image_url) values (N'Ô Long Đào Quế Hoa Kem Cheese', '38.000', 3, 'https://tocotocotea.com/wp-content/uploads/2024/04/Oolong-Dao-Que-Hoa-Kem-Cheese.png');
insert into products(product_name,price, category_id,image_url) values (N'Phê Sữa Kem Cheese', '38.000', 3, 'https://tocotocotea.com/wp-content/uploads/2024/04/phe-sua-kem-cheese.png');
insert into products(product_name,price, category_id,image_url) values (N'Ô Long Sữa Kem Café Trân Châu Sợi', '38.000', 3, 'https://tocotocotea.com/wp-content/uploads/2024/04/O-long-sua-kem-cafe-tran-chau-soi.png');
insert into products(product_name,price, category_id,image_url) values (N'Ô Long Mận Kem Phô Mai', '25.000', 3, 'https://tocotocotea.com/wp-content/uploads/2023/05/O-Long-Man-Kem-Pho-Mai.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Ô Long Kem Phô Mai', '25.000', 3, 'https://tocotocotea.com/wp-content/uploads/2021/01/oolong-kem-pho-mai_75e8d3f11fb3402196416da77c8ff33a_grande.png');
insert into products(product_name,price, category_id,image_url) values (N'Dâu Tằm Kem Phô Mai', '38.000', 3, 'https://tocotocotea.com/wp-content/uploads/2021/01/dau-tam-kem-pho-mai.png');
insert into products(product_name,price, category_id,image_url) values (N'Hồng Trà Kem Phô Mai', '25.000', 3, 'https://tocotocotea.com/wp-content/uploads/2021/01/Hong-Tra-Kem-Pho-Mai-2-copy.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Trà Xanh Kem Phô Mai', '25.000', 3, 'https://tocotocotea.com/wp-content/uploads/2021/01/Tra-Xanh-Kem-Pho-Mai-2-copy.jpg');

-- chèn dữ liệu danh mục Caffe - category_id 4
insert into products(product_name,price, category_id,image_url) values (N'Cà Phê Đen Đá', '18.000', 4, 'https://tocotocotea.com/wp-content/uploads/2023/07/9501a1967827a879f136.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Cà Phê Sữa Đá', '18.000', 4, 'https://tocotocotea.com/wp-content/uploads/2023/07/170b2e9cf72d27737e3c.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Jelly Milk Coffee', '25.000', 4, 'https://tocotocotea.com/wp-content/uploads/2021/11/Jelly-coffee.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Cheese Milk Coffee', '25.000', 4, 'https://tocotocotea.com/wp-content/uploads/2021/11/cheese-milk-coffee.png');

-- chèn dữ liệu danh mục Ice Cream - category_id 5
insert into products(product_name,price, category_id,image_url) values (N'Kem Vani Trà Sữa Trân Châu Hoàng Kim', '25.000', 5, 'https://tocotocotea.com/wp-content/uploads/2024/08/Kem-Vani-Tra-Sua-Tran-Chau-Hoang-Kim.png');
insert into products(product_name,price, category_id,image_url) values (N'Kem Trà Sữa Trân Châu Hoàng Kim', '25.000', 5, 'https://tocotocotea.com/wp-content/uploads/2024/08/Kem-Tra-Sua-Tran-Chau-Hoang-Kim.png');
insert into products(product_name,price, category_id,image_url) values (N'Kem Ly Vani Dâu', '25.000', 5, 'https://tocotocotea.com/wp-content/uploads/2023/08/z4617754537982_689908f83d0785da485d70d0307e5130.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Cafe Kem Trân Châu Hoàng Kim', '25.000', 5, 'https://tocotocotea.com/wp-content/uploads/2023/05/CF-Kem-Tran-Chau-HK.jpg');
insert into products(product_name,price, category_id,image_url) values (N'Kem Trân Châu Hoàng Kim', '25.000', 5, 'https://tocotocotea.com/wp-content/uploads/2023/05/kem-tran-chau-hoang-kim.jpg');

-- chen du lieu danh muc Special Menu - category_id 6
insert into products(product_name, price, category_id, image_url) values (N'Xanh Nhài Mãng Cầu Xoài','36.000',6,'https://tocotocotea.com/wp-content/uploads/2024/06/Xanh-Nhai-Mang-Cau-Xoai.png');
insert into products(product_name, price, category_id, image_url) values (N'Xanh Nhài Sữa Dừa Phô Mai Tươi','38.000',6,'https://tocotocotea.com/wp-content/uploads/2024/06/Xanh-Nhai-Sua-Dua-Pho-Mai-Tuoi.png');
insert into products(product_name, price, category_id, image_url) values (N'Xanh Nhài Sữa Tươi Toco','30.000',6,'https://tocotocotea.com/wp-content/uploads/2024/06/Xanh-Nhai-Sua-Toco_Tea.png');
insert into products(product_name, price, category_id, image_url) values (N'Ô Long Sữa Tươi','35.000',6,'https://tocotocotea.com/wp-content/uploads/2024/04/O-long-sua-tuoi.png');

--chen du lieu danh muc Mon noi bat - category_id 7
insert into products(product_name, price, category_id, image_url) values (N'Sữa Tươi Yến Mạch','39.000',7,'https://tocotocotea.com/wp-content/uploads/2024/10/Sua-Tuoi-Yen-Mach.png');
insert into products(product_name, price, category_id, image_url) values (N'Sữa tươi Nếp Cẩm','38.000',7,'https://tocotocotea.com/wp-content/uploads/2024/10/Sua-Tuoi-Nep-Cam.png');
insert into products(product_name, price, category_id, image_url) values (N'Ô Long Yến Mạch','25.000',7,'https://tocotocotea.com/wp-content/uploads/2024/10/O-Long-Yen-Mach.png');
insert into products(product_name, price, category_id, image_url) values (N'Trà Sữa Yến Mạch','25.000',7,'https://tocotocotea.com/wp-content/uploads/2024/10/Tra-Sua-Yen-Mach.png');
insert into products(product_name, price, category_id, image_url) values (N'Trà Sữa Nếp Cẩm','25.000',7,'https://tocotocotea.com/wp-content/uploads/2024/10/Tra-Sua-Nep-Cam.png');
insert into products(product_name, price, category_id, image_url) values (N'Thạch Đào Tiên - Peach Jelly','35.000',7,'https://tocotocotea.com/wp-content/uploads/2024/09/z5849639195288_ab6be655af86d11d4dd5f446031cb465.jpg');
insert into products(product_name, price, category_id, image_url) values (N'Kem Vani Trà Sữa Trân Châu Hoàng Kim','25.000',7,'https://tocotocotea.com/wp-content/uploads/2024/08/Kem-Vani-Tra-Sua-Tran-Chau-Hoang-Kim.png');
insert into products(product_name, price, category_id, image_url) values (N'Kem Trà Sữa Trân Châu Hoàng Kim','25.000',7,'https://tocotocotea.com/wp-content/uploads/2024/08/Kem-Tra-Sua-Tran-Chau-Hoang-Kim.png');
insert into products(product_name, price, category_id, image_url) values (N'Ô Long Đào Lê Tây Bắc Khổng Lồ - 1000ml','35.000',7,'https://tocotocotea.com/wp-content/uploads/2024/08/O-long-dao-le-tay-bac-khong-lo.png');
insert into products(product_name, price, category_id, image_url) values (N'Xanh Nhài Lê Tây Bắc Khổng Lồ - 1000ml','35.000',7,'https://tocotocotea.com/wp-content/uploads/2024/08/Xanh-Nhai-Le-Tay-Bac-Khong-Lo.png');
insert into products(product_name, price, category_id, image_url) values (N'Ô Long Đào Lê Tây Bắc','25.000',7,'https://tocotocotea.com/wp-content/uploads/2024/08/O-Long-Dao-Le-Tay-Bac.png');
insert into products(product_name, price, category_id, image_url) values (N'Xanh Nhài Lê Tây Bắc','25.000',7,'https://tocotocotea.com/wp-content/uploads/2024/08/Xanh-Nhai-Le-Tay-Bac.png');


INSERT INTO users (first_name, last_name, username, email, password, phone, role)
VALUES 
    (N'Tran', N'Anh', 'anht', 'anht@gmail.com', 'password123', '0981234567', 'User'),
    (N'Le', N'Tuan', 'tuanl', 'tuanl@gmail.com', 'password456', '0989876543', 'User'),
    (N'Nguyen', N'Linh', 'linhn', 'linhn@gmail.com', 'password789', '0987654321', 'Admin');

	INSERT INTO orders (order_date, total_amount, status, user_id)
VALUES 
    ('2024-11-01', 50000, 1, 1),
    ('2024-11-02', 75000, 0, 2),
    ('2024-11-03', 100000, 1, 1),
    ('2024-11-04', 125000, 0, 3);

	INSERT INTO order_details (order_id, product_id, quantity, temperature, size, sugar, ice)
VALUES 
    (1, 1, 2, N'Lạnh', 'M', N'50%', N'70%'),
    (1, 2, 1, N'Nóng', 'L', N'30%', N'0%'),
    (2, 3, 3, N'Lạnh', 'M', N'70%', N'50%'),
    (3, 4, 1, N'Lạnh', 'S', N'100%', N'30%'),
    (4, 5, 2, N'Lạnh', 'L', N'50%', N'100%');

	INSERT INTO delivery (customer_name, customer_phone, customer_location, customer_note, order_id)
VALUES 
    (N'Tran Anh', '0981234567', N'123 Đường ABC, Quận 1', N'Giao trong buổi sáng', 1),
    (N'Le Tuan', '0989876543', N'456 Đường XYZ, Quận 2', NULL, 2),
    (N'Nguyen Linh', '0987654321', N'789 Đường QWE, Quận 3', N'Giao vào buổi chiều', 3),
    (N'Phan Duy', '0976543210', N'101 Đường RTY, Quận 4', N'Liên hệ trước khi giao', 4);


select *from products 


 update products
 set price = price * 1000;



