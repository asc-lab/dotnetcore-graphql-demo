CREATE TABLE IF NOT EXISTS developers (
   id  UUID PRIMARY KEY,
   name character varying(150) NOT NULL,
   rate numeric(19,2) NOT NULL
);


ALTER TABLE project_tasks ADD hours_work numeric(19,2);

ALTER TABLE project_tasks ADD cost numeric(19,2);

ALTER TABLE project_tasks ADD assignee_id UUID;

ALTER TABLE project_tasks ADD CONSTRAINT FK_project_task_assignee_id FOREIGN KEY (assignee_id) REFERENCES developers(id)
    ON DELETE CASCADE;

CREATE INDEX idx_project_tasks_assignee_id ON project_tasks(assignee_id);

ALTER TABLE project_tasks ADD status character varying(50);