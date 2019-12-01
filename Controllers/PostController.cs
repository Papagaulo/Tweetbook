using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TweetBook.Services;
using TweetBook.DTOS.RequestDTOS;
using TweetBook.Domains;

namespace TweetBook.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository repository;

        public PostController(IPostRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Get()
        {
            return Ok(await repository.GetPosts());
        }
        [HttpGet("{postId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get([FromRoute] string postId)
        {
            var post = await repository.GetPost(Guid.Parse(postId));
            if (post != null)
            {
                return Ok(post);
            }
            return NotFound(value: postId);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] PostRequest postRequest)
        {
            var post = new Post
            {
                Name = postRequest.Name
            };
            if (await repository.CreatePost(post))
            {
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                var locationUrl = $"{baseUrl}/v1/post/{post.ID}";
                return Created(locationUrl, post);
            }
            return BadRequest(postRequest);
        }

        [HttpPatch("{postId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdatePost([FromRoute] string postId, [FromBody] PostRequest postChanges)
        {
            var post = new Post
            {
                ID = Guid.Parse(postId),
                Name = postChanges.Name
            };
            if (await repository.UpdatePost(post))
            {
                return Ok(post);
            };
            return NotFound(postId);
        }

        [HttpDelete("{postId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeletePost([FromRoute] string postId)
        {
            if (await repository.DeletePost(Guid.Parse(postId)))
            {
                return Ok(postId);
            };
            return NotFound(postId);
        }
    }
}