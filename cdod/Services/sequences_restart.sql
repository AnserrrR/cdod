select nextval('"Students_Id_seq"');
select max(S."Id") from "Students" S where S."Id" is not null;
ALTER SEQUENCE "Students_Id_seq" RESTART WITH 301;

select nextval('"ContractStates_Id_seq"');
select max(CS."Id") from "ContractStates" CS where CS."Id" is not null;
ALTER SEQUENCE "ContractStates_Id_seq" RESTART WITH 11;

select nextval('"Courses_Id_seq"');
select max(C."Id") from "Courses" C where C."Id" is not null;
ALTER SEQUENCE "Courses_Id_seq" RESTART WITH 13;

select nextval('"Users_Id_seq"');
select max(U."Id") from "Users" U where U."Id" is not null;
ALTER SEQUENCE "Users_Id_seq" RESTART WITH 201;

select nextval('"Groups_Id_seq"');
select max(G."Id") from "Groups" G where G."Id" is not null;
ALTER SEQUENCE "Groups_Id_seq" RESTART WITH 21;

select nextval('"PayNotes_Id_seq"');
select max(PN."Id") from "PayNotes" PN where PN."Id" is not null;
ALTER SEQUENCE "PayNotes_Id_seq" RESTART WITH 2001;

