# Hướng dẫn sử dụng thư viện vẽ Schelling Model cho Bài tập 2017

Các bạn bấm nút **Clone and Download** (màu xanh lá cây ở trên bên phải), rồi chọn **Download ZIP** để tải mã nguồn về.

Thư viện vẽ Schelling Model cho Bài tập 2017 được đặt tại thư mục **Bài tập \ 2017**. Các bạn mở Project SchellingModel để xem code và biết cách sử dụng.

Trong Project SchellingModel bao gồm 2 file:
- GridUtility.cs - thư viện chứa 1 hàm để vẽ cái lưới ra Console 
- Program.cs - các em xem cách gọi sử dụng thư viện GridUtility

**Cách thêm thư viện này vào code của bạn:**
- Trong project của bạn, nháy phải chuột lên tên project, chọn mục Add\Existing item...
- Trong hộp thoại mở ra, chọn đến thư mục project SchellingModel của thầy, chọn file GridUtility.cs, bấm nút Add

Kết quả là file GridUtility.cs được thêm vào project của bạn.

**Cách gọi hàm DrawGrid trong lớp GridUtility**

Hàm DrawGrid nhận vào một mảng hai chiều. Tại mỗi ô, nếu là giá trị 1 sẽ vẽ ra chữ X, nếu là 0 sẽ vẽ ra chữ O.

Do đó, bạn cần chuyển lưới mô hình Schelling của bạn thành mảng 2 chiều theo quy định trên. Bạn có thể tự quy định X, O là màu da nào cũng được (Nên nói rõ trong báo cáo). 
