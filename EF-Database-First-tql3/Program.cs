using Microsoft.EntityFrameworkCore;

namespace EF_Database_First_tql3;

internal class Program {

    static void Main(string[] args) {
        var _context = new TqlprsdbContext();

        var newRequest = (from r in _context.Requests
                         join rl in _context.Requestlines
                            on r.Id equals rl.RequestId
                         join p in _context.Products
                             on rl.ProductId equals p.Id
                         join v in _context.Vendors
                             on p.VendorId equals v.Id
                         join u in _context.Users
                             on r.UserId equals u.Id
                         where r.Description == "I need a ROLEX!!!"
                         select new {
                             r, rl, p, u, v
                             Description = r.Description, 
                             Name = p.Name, 
                             Quantity = rl.Quantity, 
                             Price = p.Price
                         }).First();
        Console.WriteLine(
            $"{newRequest.Description} | {newRequest.Name} | " +
            $" {newRequest.Quantity} | {newRequest.Price}"
        );

    }
    static void AddRowsToDb() { 
        var _context = new TqlprsdbContext();

        var newUser = new User {
            Id = 0, Username = "gd", Password = "gd",
            Firstname = "Greg", Lastname = "Doud",
            IsReviewer = true, IsAdmin = true
        };
        _context.Users.Add(newUser);

        var newVendor = new Vendor {
            Id = 0, Code = "MAX", Name = "MAX", Address = "123 Any Street",
            City = "Mason", State = "OH", Zip = "45040"
        };
        _context.Vendors.Add(newVendor);
        _context.SaveChanges();

        var newProduct = new Product {
            Id = 0, PartNbr = "ROLEX", Name = "Rolex",
            Price = 35000, Unit = "Each", VendorId = 3
        };
        _context.Products.Add(newProduct);
        _context.SaveChanges();

        var newRequest = new Request {
            Id = 0, Description = "I need a ROLEX!!!",
            Justification = "Just Because!", DeliveryMode = "Pickup",
            Status = "NEW", Total = 0, 
            UserId = _context.Users.Single(x => x.Username == "GD").Id
        };
        _context.Requests.Add(newRequest);
        _context.SaveChanges();

        var newRequestline1 = new Requestline {
            Id = 0, Quantity = 1, RequestId = 2, ProductId = 10
        };
        _context.Requestlines.Add(newRequestline1);
        
        var newRequestline2 = new Requestline {
            Id = 0, Quantity = 1, RequestId = 2, ProductId = 1
        };
        _context.Requestlines.Add(newRequestline2);
        _context.SaveChanges();

    }
}
