namespace TD.WebApi.Domain.Catalog;

/// <summary>
/// Loại phương tiẹn
/// </summary>
public class VehicleType : AuditableEntity, IAggregateRoot
{
    public string Name { get;  set; }
    /// <summary>
    /// Loai phuong tien
    /// 0: Khac
    /// 1: Xe Dap
    /// 2: Xe may
    /// 3: Oto
    /// </summary>
    public int Type { get; set; } = 0;

    public int? TinhTienVeThang { get; set; } = 0;
    public int? DonGiaThang { get; set; } = 0;
    public int? PhiCapLoaiThe { get; set; } = 0;
    public int? PhiLamMoiThe { get; set; } = 0;
    public int? DonGiaLuot { get; set; } = 0;
    public int? DonGiaBlock { get; set; } = 0;
    public int? ThoiGianMienPhi { get; set; } = 0;
    public int? ThoiGianDongGia { get; set; } = 0;
    public int? ThoiGianBlock { get; set; } = 0;


    /// <summary>
    /// Loại tính tiền vé lượt
    /// 0: Tính tiền theo lượt
    /// 1: Tính tiền theo block
    /// </summary>
    public int? LoaiTinhTienVeLuot { get; set; } = 0;


    public Guid? BranchId { get; set; }
    public string? Description { get;  set; }

    public VehicleType(string name, int type, int? tinhTienVeThang, int? donGiaThang, int? phiCapLoaiThe, int? phiLamMoiThe, int? donGiaLuot, int? donGiaBlock, int? thoiGianMienPhi, int? thoiGianDongGia, int? thoiGianBlock, int? loaiTinhTienVeLuot, DefaultIdType? branchId, string? description)
    {
        Name = name;
        Type = type;
        TinhTienVeThang = tinhTienVeThang;
        DonGiaThang = donGiaThang;
        PhiCapLoaiThe = phiCapLoaiThe;
        PhiLamMoiThe = phiLamMoiThe;
        DonGiaLuot = donGiaLuot;
        DonGiaBlock = donGiaBlock;
        ThoiGianMienPhi = thoiGianMienPhi;
        ThoiGianDongGia = thoiGianDongGia;
        ThoiGianBlock = thoiGianBlock;
        LoaiTinhTienVeLuot = loaiTinhTienVeLuot;
        BranchId = branchId;
        Description = description;
    }

    public VehicleType Update(string name, int type, int? tinhTienVeThang, int? donGiaThang, int? phiCapLoaiThe, int? phiLamMoiThe, int? donGiaLuot, int? donGiaBlock, int? thoiGianMienPhi, int? thoiGianDongGia, int? thoiGianBlock, int? loaiTinhTienVeLuot, DefaultIdType? branchId, string? description)
    {
        Name = name;
        Type = type;
        TinhTienVeThang = tinhTienVeThang;
        DonGiaThang = donGiaThang;
        PhiCapLoaiThe = phiCapLoaiThe;
        PhiLamMoiThe = phiLamMoiThe;
        DonGiaLuot = donGiaLuot;
        DonGiaBlock = donGiaBlock;
        ThoiGianMienPhi = thoiGianMienPhi;
        ThoiGianDongGia = thoiGianDongGia;
        ThoiGianBlock = thoiGianBlock;
        LoaiTinhTienVeLuot = loaiTinhTienVeLuot;
        Description = description;
        return this;
    }
}