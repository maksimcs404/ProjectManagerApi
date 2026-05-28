   - `dotnet ef database update`
3. Start API:
   - `dotnet run --project ProjectManager`
## Auth
- Login returns JWT token.
- Use token in header:
  - `Authorization: Bearer <token>`
## Role Model
Project member roles:
- `Owner` - full access
- `Member` - write access
- `Viewer` - read-only access
Task endpoints enforce project access:
- read access for reading comments
- write access for creating tasks/comments/likes
## Endpoints
### Auth (`/Auth`)
- `POST /Auth/register` - register user
- `POST /Auth/login` - login and get JWT
### User (`/User`) `[Authorize]`
- `GET /User/me` - get current user
- `DELETE /User/me` - delete current user
### Project (`/Project`) `[Authorize]`
- `POST /Project/create` - create project
- `PUT /Project/{id}` - update project (owner only)
- `GET /Project/projects` - get my projects
- `POST /Project/{id}/members` - add member with role (owner only)
- `DELETE /Project/{projectId}/members/{memberId}` - delete project member (owner only)
- `DELETE /Project/{id}` - delete project (owner only)
- ### Task (`/Task`) `[Authorize]`
- `POST /Task/{ProjectId}/create` - create task (write access)
- `GET /Task` - get user tasks
- `POST /Task/{taskId}/comments` - add task comment (write access)
- `GET /Task/{taskId}/comments` - get task comments (read access)
- `POST /Task/comments/{commentId}/likes` - like comment (write access)
## Notes
- Connection string is read via `ConnectionStrings:DefaultConnection` (supports env var `ConnectionStrings__DefaultConnection`).
- Current tests are in `ProjectManager.Tests`.
## First Project
- This is my first project on GitHub, and I am still learning how to use it.

