CREATE TABLE IF NOT EXISTS project_tasks (
   id  UUID PRIMARY KEY,
   description character varying(150) NOT NULL,
   project_id UUID NOT NULL
); 

ALTER TABLE project_tasks ADD CONSTRAINT FK_project_task_project_id FOREIGN KEY (project_id) REFERENCES projects(id) 
ON DELETE CASCADE;

CREATE INDEX idx_project_tasks_project_id ON project_tasks(project_id); 