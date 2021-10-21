using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MyInventory.Data;
using MyInventory.Models;

namespace MyInventory.Controllers
{
    public class ItemController : Controller
    {
        //Dependency injection
        private readonly ApplicationDbContext _context;
        public ItemController(ApplicationDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            //declare a variable list that displays records from the Items table. The model will be included to be rendered by the View method
            var list = _context.Items.ToList();

            return View(list);
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Item record)
        {
            // initialize a variable item from the Item class
            var item = new Item();
            item.Name = record.Name;
            item.Code = record.Code;
            item.Description = record.Description;
            item.Price = record.Price;
            item.DateAdded = DateTime.Now;
            item.Type = record.Type;

            _context.Items.Add(item);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            //Edit method using a nullable integer id as the parameter. 
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            // variable item that gets the existing record from the Items table.
            var item = _context.Items.Where(i => i.ItemId == id).SingleOrDefault();

            //checks if the item record is not present. If the condition is valid, the view will redirect to the Index action
            if (item == null)
            {
                return RedirectToAction("Index");
            }
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(int? id, Item record)
        {
            
            // a variable item that gets the existing record from the Items table based on the parameter id value.
            var item = _context.Items.Where(i => i.ItemId == id).SingleOrDefault();

            //Identify the list of properties and set their value from the record parameter.
            item.Name = record.Name;
            item.Code = record.Code;
            item.Description = record.Description;
            item.Price = record.Price;
            item.DateModified = DateTime.Now;
            item.Type = record.Type;

            //Update the existing values, then save the record from the database table. 
            _context.Items.Update(item);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            // checks if a valid value is not present. If the condition is valid, the view will redirect to the Index action
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            //Declare a variable item that gets the existing record from the Items table. 
            var item = _context.Items.Where(i => i.ItemId == id).SingleOrDefault();

            //checks if the item record is not present. If the condition is valid, the view will redirect to the Index action
            if (item == null)
            {
                return RedirectToAction("Index");
            }

            //Remove the existing record from the database table. 
            _context.Items.Remove(item);
            _context.SaveChanges();

            //Return the view redirecting back to the Index action.
            return RedirectToAction("Index");
        }
    }
}
