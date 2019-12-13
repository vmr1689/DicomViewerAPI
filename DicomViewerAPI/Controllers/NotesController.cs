using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DicomViewerAPI.Models;
using DicomViewerAPI.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DicomViewerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IDataRepository<Notes> _dataRepository;
        public NotesController(IDataRepository<Notes> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Notes> notes = _dataRepository.GetAll();
            return Ok(notes);
        }

        [HttpGet("GetByInstanceId/{instanceId}", Name = "GetByInstanceId")]
        public IActionResult GetByInstanceId(string instanceId)
        {
            IEnumerable<Notes> notes = _dataRepository.GetByInstanceId(instanceId);

            if (notes == null)
            {
                return NotFound("The Notes record couldn't be found.");
            }

            return Ok(notes);
        }

        [HttpGet("GetByNotesId/{notesId}", Name = "GetByNotesId")]
        public IActionResult GetByNotesId(long notesId)
        {
            Notes notes = _dataRepository.GetByNotesId(notesId);

            if (notes == null)
            {
                return NotFound("The Notes record couldn't be found.");
            }

            return Ok(notes);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Notes notes)
        {
            if (notes == null)
            {
                return BadRequest("Notes is null.");
            }

            _dataRepository.Add(notes);
            return CreatedAtRoute(
                  "GetByNotesId",
                  new { notesId = notes.NotesId },
                  notes);
        }

        [HttpPut("{notesId}")]
        public IActionResult Put(long notesId, [FromBody] Notes notes)
        {
            if (notes == null)
            {
                return BadRequest("Notes is null.");
            }

            Notes notesToUpdate = _dataRepository.GetByNotesId(notesId);
            if (notesToUpdate == null)
            {
                return NotFound("The notes record couldn't be found.");
            }

            _dataRepository.Update(notesToUpdate, notes);
            return NoContent();
        }

        [HttpDelete("{notesId}")]
        public IActionResult Delete(long notesId)
        {
            Notes notes = _dataRepository.GetByNotesId(notesId);
            if (notes == null)
            {
                return NotFound("The notes record couldn't be found.");
            }

            _dataRepository.Delete(notes);
            return NoContent();
        }
    }
}