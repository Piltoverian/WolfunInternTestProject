# Wolfun Intern Test Project - Survival Game

## 1. Thông tin chung
* **Thể loại:** Top-down Survival
* **Góc nhìn:** Top-down, Camera follow nhân vật
* **Nền tảng:** Windows
* **Unity Version:** 6000.3.13f1

## 2. Hướng dẫn Mở và Chơi game
### Chơi ngay bằng bản Build:
1. Giải nén thư mục bản Build Windows đính kèm.
2. Chạy file thực thi `.exe` (ví dụ: `WolfunInternTestProject.exe`) để mở game và chơi trực tiếp.

### Mở bằng Unity Editor (Kiểm tra mã nguồn):
1. Trong cửa sổ Project, mở thư mục `Assets/Scenes/`.
2. Double-click vào file `Menu.unity` để load màn hình Menu chính.
3. Nhấn nút **Play (▶)** ở cạnh trên màn hình Editor để bắt đầu test game. (Project cam kết không có lỗi Compile).

## 3. Phím / Nút điều khiển
* **Di chuyển:** Sử dụng cụm phím `W A S D` trên bàn phím (hoặc kéo Joystick ảo trên màn hình).
* **Kỹ năng 1 (Đánh thường):** Bắn 3 viên đạn hình nón (Click vào nút Kỹ năng 1 trên UI).
* **Kỹ năng 2 (Đặt Bom):** Đặt bom tại chỗ, nổ sau 2s (Click nút Kỹ năng 2).
* **Kỹ năng 3 (Dash):** Lướt nhanh về phía trước và nổ (Click nút Kỹ năng 3).

## 4. Danh sách các phần đã làm được
### Bắt buộc (Core Gameplay - 100%)
- **Player:** Di chuyển, xoay người 180 độ/s, quản lý HP, Giáp, và Damage Multiplier. (Đã làm)
- **Combat & Skills:** Đánh thường (charge/cooldown), Bom (AOE delay), Dash (di chuyển + AOE). Hoạt động đúng công thức sát thương. (Đã làm)
- **Enemy:** Quái cận chiến (Melee) và đánh xa (Ranged ném độc), AI sử dụng NavMesh tìm đường, đúng chu kỳ tấn công và khoảng cách. (Đã làm)
- **Status Effect:** Hiệu ứng độc rút máu theo thời gian (DoT) chuẩn 4 tick/3s, reset không stack. (Đã làm)
- **Wave & Cấp độ:** Wave Manager sinh quái ngẫu nhiên, hệ thống EXP (100 exp/level) và cộng dồn chỉ số khi lên cấp. (Đã làm)
- **UI:** Overlay HUD (Máu, Level), World Space Enemy HP, Skill Buttons với Cooldown/Charge, Joystick. (Đã làm)
- **Tối ưu code (Optimization):** Áp dụng Object Pooling bằng Dictionary/Queue (hạn chế Instantiate/Destroy). Code tách biệt module rõ ràng. (Đã làm)

### Điểm cộng (Bonus)
- **Camera Shake:** (Chưa làm)
- **VFX (Hiệu ứng hình ảnh):** (Chưa làm)
- **Âm thanh (SFX):** (Chưa làm)

---
*Dự án thực hiện cho bài test thực tập sinh Wolffun.*
