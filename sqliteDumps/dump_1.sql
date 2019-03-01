BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Billings" (
	"Id"	BLOB NOT NULL,
	"AppointmentId"	BLOB NOT NULL,
	"Price"	TEXT NOT NULL,
	CONSTRAINT "PK_Billings" PRIMARY KEY("Id"),
	CONSTRAINT "FK_Billings_Appointments_AppointmentId" FOREIGN KEY("AppointmentId") REFERENCES "Appointments"("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "UserSettings" (
	"Id"	BLOB NOT NULL,
	"ClientId"	BLOB NOT NULL,
	"PreferLanguage"	TEXT,
	CONSTRAINT "PK_UserSettings" PRIMARY KEY("Id")
);
CREATE TABLE IF NOT EXISTS "Therapists" (
	"Id"	BLOB NOT NULL,
	"Name"	TEXT NOT NULL,
	"Gender"	INTEGER NOT NULL,
	"MobileNumber"	TEXT NOT NULL,
	"Email"	TEXT NOT NULL,
	"Address"	TEXT,
	"Username"	TEXT,
	"PasswordHash"	BLOB,
	"PasswordSalt"	BLOB,
	CONSTRAINT "PK_Therapists" PRIMARY KEY("Id")
);
CREATE TABLE IF NOT EXISTS "Clients" (
	"Id"	BLOB NOT NULL,
	"Name"	TEXT NOT NULL,
	"Gender"	INTEGER NOT NULL,
	"MobileNumber"	TEXT NOT NULL,
	"Email"	TEXT NOT NULL,
	"Address"	TEXT,
	"CivilStatus"	INTEGER NOT NULL,
	"DateOfBirth"	TEXT NOT NULL,
	"NIF"	TEXT,
	"Occupation"	TEXT,
	"Observations"	TEXT,
	CONSTRAINT "PK_Clients" PRIMARY KEY("Id")
);
CREATE TABLE IF NOT EXISTS "AppointmentType" (
	"Id"	BLOB NOT NULL,
	"Name"	TEXT NOT NULL,
	"Code"	TEXT NOT NULL,
	CONSTRAINT "PK_AppointmentType" PRIMARY KEY("Id")
);
CREATE TABLE IF NOT EXISTS "Appointments" (
	"Id"	BLOB NOT NULL,
	"AppointmentTypeId"	BLOB NOT NULL,
	"ClientId"	BLOB NOT NULL,
	"TherapistId"	BLOB NOT NULL,
	"AppointmentDate"	TEXT NOT NULL,
	CONSTRAINT "PK_Appointments" PRIMARY KEY("Id")
);
INSERT INTO "Therapists" ("Id","Name","Gender","MobileNumber","Email","Address","Username","PasswordHash","PasswordSalt") VALUES (']�����XN��-`��','Tiago Sá',77,'123654789','sa@email.com','Rua X','tiagossa1','PC=�j)�z�Ǯ��,y��R���?���ןc�hU�ŉ������g�����g��jzov���','h!
YN7-"�2�u��q���]�l�Q&��.?����A�@����X�C���>x3���#�-?���ԓ;�@q^���`ft�~��49F�6�þ�?����ͫ��ϯ�ej��K�`����?5���hX����');
INSERT INTO "Clients" ("Id","Name","Gender","MobileNumber","Email","Address","CivilStatus","DateOfBirth","NIF","Occupation","Observations") VALUES ('�˔��D�{��#��I','Sá',77,'321654987','sa@email.com','string',67,'2000-10-30 00:00:00','321654789','A','string');
INSERT INTO "AppointmentType" ("Id","Name","Code") VALUES ('c �~H�����w�','Florais','FLORAIS');
INSERT INTO "AppointmentType" ("Id","Name","Code") VALUES ('qi말=E����Fݎ','Acupuntura','ACUPUNTURA');
INSERT INTO "AppointmentType" ("Id","Name","Code") VALUES ('�v7��aD����H��=','Cromoterapia','CROMOTERAPIA');
INSERT INTO "AppointmentType" ("Id","Name","Code") VALUES ('�� �Y4C�SWk�0�','Massagem','MASSAGEM');
INSERT INTO "AppointmentType" ("Id","Name","Code") VALUES ('���yI�\Z0�o�6','Terapia Com Flores','TERAPIACOMFLORES');
INSERT INTO "AppointmentType" ("Id","Name","Code") VALUES ('x-��6TBK��0!@�','Fitoterapia','FITOTERAPIA');
INSERT INTO "AppointmentType" ("Id","Name","Code") VALUES ('ۚ1�B��]tO�','Reflexologia','REFLEXOLOGIA');
INSERT INTO "AppointmentType" ("Id","Name","Code") VALUES ('n0��<�jB�Ҥ�n#','Shiatsu','SHIATSU');
INSERT INTO "AppointmentType" ("Id","Name","Code") VALUES ('b1�)���F���}���q','Reiki','REIKI');
INSERT INTO "Appointments" ("Id","AppointmentTypeId","ClientId","TherapistId","AppointmentDate") VALUES ('�&/B�L����)Q]','qi말=E����Fݎ','�˔��D�{��#��I',']�����XN��-`��','2019-02-14 16:00:00');
INSERT INTO "Appointments" ("Id","AppointmentTypeId","ClientId","TherapistId","AppointmentDate") VALUES (X'8412485597005d4eb4a55f4059033bad','�v7��aD����H��=','�˔��D�{��#��I',']�����XN��-`��','2019-07-16 17:00:00');
CREATE INDEX IF NOT EXISTS "IX_Billings_AppointmentId" ON "Billings" (
	"AppointmentId"
);
COMMIT;
