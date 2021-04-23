CREATE TABLE IF NOT EXISTS project_tasks (
   id  UUID PRIMARY KEY,
   description character varying(150) NOT NULL,
   project_id UUID NOT NULL
); 