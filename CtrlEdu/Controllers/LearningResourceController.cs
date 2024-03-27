using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CtrlEdu.Data;
using CtrlEdu.Models;
using System.Threading.Tasks;

public class LearningResourceController : Controller
{
    private readonly ApplicationDbContext _context;

    public LearningResourceController(ApplicationDbContext context)
    {
        _context = context;
    }

    // ...

    // Action to add a YouTube link material to a course
    [HttpPost]
    public async Task<IActionResult> AddYouTubeLink(int courseId, string title, string youtubeLink)
    {
        // Find the course
        var course = await _context.Courses
            .Include(c => c.LearningResources)
            .FirstOrDefaultAsync(c => c.CourseID == courseId);

        if (course == null)
        {
            return NotFound();
        }

        // Create a new LearningResourceModel for the YouTube link
        var youtubeResource = new LearningResourceModel
        {
            CourseID = courseId,
            Title = title,
            Type = "YouTube", // Assuming "YouTube" as the resource type
            Content = youtubeLink // Store the YouTube link
        };

        // Add the YouTube link material to the course
        _context.LearningResources.Add(youtubeResource);
        await _context.SaveChangesAsync();

        return RedirectToAction("CourseDetail", new { id = courseId });
    }
}
