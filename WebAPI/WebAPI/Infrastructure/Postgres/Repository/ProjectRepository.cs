using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Core.Entities;
using WebAPI.Core.Interfaces.Database;

namespace WebAPI.Infrastructure.Postgres.Repository
{
    public class ProjectRepository : BaseCrudRepository<DatabaseContext, Project>, IProjectRepository
    {
        public ProjectRepository(DatabaseContext databaseContext) : base(databaseContext) { }
        
        public async Task<List<Project>> GetFullProjectByUserId(Guid userId)
        {
            var query = from users in _dbContext.Users
                join teams in _dbContext.Teams on users.TeamId equals teams.TeamId
                join projects in _dbContext.Projects
                        .Include(x => x.Epics)
                        .Include(x => x.Teams)
                    on teams.ProjectId equals projects.ProjectId
                where users.UserId == userId select projects;

            var epics = await query.ToListAsync();

            return epics;
        }
    }
}