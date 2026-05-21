using CommunityToolkit.Mvvm.ComponentModel;
using SOK_WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOK_WPF.ViewModels
{
    [ObservableObject]
    public partial class ActiveAdminsVM
    {
        [ObservableProperty]
        List<Admin>? admins = new List<Admin>()
        {
            new Admin { Id = 1, Name = "Jan", Role = "SuperAdmin", Email = "jan@test.pl" },
            new Admin { Id = 2, Name = "Anna", Role = "Moderator", Email = "anna@test.pl" },
            new Admin { Id = 3, Name = "Piotr", Role = "Admin", Email = "piotr@test.pl" }
        };


        public ActiveAdminsVM()
        {
        }
    }
}
