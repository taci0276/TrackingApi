#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackingApi.Models;
using TrackingApi.DTOs;

namespace TrackingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly TrackingContext _context;

        public ActivitiesController(TrackingContext context)
        {
            _context = context;
        }

        // GET: api/Activities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
        {
            return await _context.Activities.ToListAsync();
        }

// cu ambi parametri in metoda...dar merge doar hours
    [Route("byHoursAndDate")]
  // GET: api/Activities/byHour
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityDTO>>> GetActivities(double hours, String startDate)
        {
            var query = _context.Activities.AsQueryable();
            query = query.Where(activity => activity.Hours == hours && activity.StartDate == startDate);
            return await query.Select(activity => ActivityToDTO(activity)).ToListAsync();
            // return await _context.Activities.ToListAsync();
        }

    [Route("byHours")]
    // GET: api/Activities/byHours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityDTO>>> GetActivitiesByHour(double hours)
        {
            var query = _context.Activities.AsQueryable();
            query = query.Where(activity => activity.Hours == hours);
            return await query.Select(activity => ActivityToDTO(activity)).ToListAsync();
        }


    [Route("byDate")]
    // GET: api/Activities/byDate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityDTO>>> GetActivitiesByDate(string startDate)
        {
            var query = _context.Activities.AsQueryable();
            query = query.Where(activity => activity.StartDate.Equals(startDate));
            return await query.Select(activity => ActivityToDTO(activity)).ToListAsync();
        }

//  public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems(bool? showOnly)
//         {
//             var query = _context.TodoItems.AsQueryable();
//             if (showOnly.HasValue)
//             {
//                 query = query.Where(item => item.IsComplete == showOnly.Value);
//             }
//             return await query.Select(item => ItemToDTO(item)).ToListAsync();
//         }


        // GET: api/Activities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(long id)
        {
            var activity = await _context.Activities.FindAsync(id);

            if (activity == null)
            {
                return NotFound();
            }

            return activity;
        }

        // PUT: api/Activities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(long id, Activity activity)
        {
            if (id != activity.Id)
            {
                return BadRequest();
            }

            _context.Entry(activity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Activities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Activity>> PostActivity(Activity activity)
        {
            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivity", new { id = activity.Id }, activity);
        }

        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(long id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActivityExists(long id)
        {
            return _context.Activities.Any(e => e.Id == id);
        }


         private static ActivityDTO ActivityToDTO(Activity activity) =>
            new ActivityDTO
            {
                Id = activity.Id,
                Name = activity.Name,
                Hours = activity.Hours,
                Description = activity.Description,
                StartDate  = activity.StartDate
            };
    }
}
