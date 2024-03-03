using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.VehicleTypes;
public class VehicleTypeDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Type { get; set; }
    public int? TinhTienVeThang { get; set; }
    public int? DonGiaThang { get; set; }
    public int? PhiCapLoaiThe { get; set; }
    public int? PhiLamMoiThe { get; set; }
    public int? DonGiaLuot { get; set; }
    public int? DonGiaBlock { get; set; }
    public int? ThoiGianMienPhi { get; set; }
    public int? ThoiGianDongGia { get; set; }
    public int? ThoiGianBlock { get; set; }
    public int? LoaiTinhTienVeLuot { get; set; }
    public Guid? BranchId { get; set; }
    public string? Description { get; set; }
}
