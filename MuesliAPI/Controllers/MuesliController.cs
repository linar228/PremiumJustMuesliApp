﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuesliCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MuesliAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MuesliController : ControllerBase
    {
        // GET: api/<MuesliController>
        [HttpGet]
        public IEnumerable<MuesliMix> Get()
        {
            return DBConnect.GetMuesliMixes();
        }

        // GET api/<MuesliController>/5
        [HttpGet("{id}")]
        public ActionResult<MuesliMix> Get(int id)
        {
            var result = DBConnect.GetMuesliMix(id);
            if (result == null)
                return NotFound();

            return result;
        }

        // POST api/<MuesliController>
        [HttpPost]
        public IActionResult Post(MuesliMix mix)
        {
            DBConnect.CreateOrder(mix);
            return NoContent();
        }

        // DELETE api/<MuesliController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DBConnect.RemoveOrder(id);
            return NoContent();
        }
    }
}