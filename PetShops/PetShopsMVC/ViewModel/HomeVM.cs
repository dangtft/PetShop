﻿using PetShopsMVC.DTOs;
using PetShopsMVC.Models;

namespace PetShopsMVC.ViewModel
{
    public class HomeVM
    {
        public IEnumerable<Products> Products { get; set; }
        public Products Product { get; set; }
        public IEnumerable<Blogs> Blogs { get; set; }
        public Blogs Blog { get; set; }
        public RegisterRequestDTO Register { get; set; }
    }

    public class BlogVM
    {
        public IEnumerable<Blogs> Blogs { get; set; }
        public Blogs Blog { set; get; }
    }
}
