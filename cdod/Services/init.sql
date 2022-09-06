alter table "PayNotes"
    add constraint paynotes_studenttocourses_studentid_courseid_attempt_fk
        foreign key ("StudentId", "CourseId",  "Attempt") references "StudentToCourses" ("StudentId", "CourseId",  "Attempt")
            on delete cascade;

CREATE OR REPLACE FUNCTION attempt_counter()
    RETURNS TRIGGER
    LANGUAGE PLPGSQL
AS
$$
BEGIN
    SELECT COALESCE(max("Attempt"), 0) + 1
    INTO NEW."Attempt"
    FROM "StudentToCourses"
    WHERE "StudentId" = NEW."StudentId"
      AND "CourseId" = NEW."CourseId";
    RETURN NEW;
END;
$$;

CREATE OR REPLACE TRIGGER attempt_counter_trigger
    BEFORE INSERT
    ON "StudentToCourses"
    FOR EACH ROW
EXECUTE FUNCTION attempt_counter();