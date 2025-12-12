-- =======================================
-- SEED SCRIPT: Maintenance Manager (Dynamic NOW)
-- =======================================
-- Includes: Customers, Machines, MaintenanceRules
-- =======================================

-- 1️⃣ Clear existing data (in correct order)
TRUNCATE TABLE 
  "MaintenanceRules",
  "Components",
  "Machines",
  "UserCustomers",
  "Customers",
  "Users"
  RESTART IDENTITY CASCADE;

-- =======================================
-- 2️⃣ Customers
-- =======================================
INSERT INTO "Customers" 
("Name", "Email", "IsActive", "CreatedDate", "Reference") 
VALUES
('SteelTech BV', 'contact@steeltech.nl', TRUE, NOW() - INTERVAL '300 days', 'CUS-20250201-001'),
('Metaalbouw Deventer', 'info@metaalbouwdev.nl', TRUE, NOW() - INTERVAL '280 days', 'CUS-20250201-002'),
('IronWorks GmbH', 'service@ironworks.de', TRUE, NOW() - INTERVAL '260 days', 'CUS-20250201-003'),
('Rotterdam Steel Co', 'support@rotsteel.nl', TRUE, NOW() - INTERVAL '240 days', 'CUS-20250201-004'),
('Nordic Fabricators', 'hello@nordicfab.no', FALSE, NOW() - INTERVAL '220 days', 'CUS-20250201-005'),
('Atlas Heavy Industries', 'contact@atlashi.eu', TRUE, NOW() - INTERVAL '200 days', 'CUS-20250201-006'),
('VDW Metalworks', 'info@vdwmetalworks.nl', TRUE, NOW() - INTERVAL '180 days', 'CUS-20250201-007'),
('Titan Constructors', 'admin@titancons.com', TRUE, NOW() - INTERVAL '160 days', 'CUS-20250201-008'),
('Helios Engineering', 'ops@helioseng.uk', TRUE, NOW() - INTERVAL '140 days', 'CUS-20250201-009'),
('WestSteel Ltd', 'info@weststeel.be', TRUE, NOW() - INTERVAL '120 days', 'CUS-20250201-010');










-- =======================================
--  Users
-- =======================================

insert into "Users"
("Reference","Name","Email","Role","IsActive","CreatedDate")
Values
('USR-ADMIN-E','Emad','Emad@admin.com',2,true,now()),
('USR-43f86a4f-0c62-4ef2-88df-74db8b1b5ab4','Tom Engineer','tom.engineer@maintsys.com',0,TRUE,NOW() - INTERVAL '2 days'),
('USR-77db3a63-3b2c-4702-bd09-c6c92cb46c2b','Laura Engineer','laura.engineer@maintsys.com',0,TRUE,NOW() - INTERVAL '3 days'),
('USR-98e6e493-4974-48cf-9b2d-13fa1dbde433','John Support','john.support@maintsys.com',3,TRUE,NOW() - INTERVAL '4 days'),
('USR-d80e8a5c-57d2-4c68-824f-53a8166ce71c','Karin Support','karin.support@maintsys.com',3,TRUE,NOW() - INTERVAL '5 days');




-- =======================================
--  UserCustomer Assignments (all to same customer)
-- =======================================
INSERT INTO "UserCustomers" ("Reference","UserReference","CustomerReference","AssignedAt")
VALUES
('USR-CUS-2a9fbe8e-3f9a-493d-b511-f83f3b1933ef','USR-ADMIN-E','CUS-20250201-001',NOW()),
('USR-CUS-4e86b537-5c7f-44a1-a8e1-5d204be38a8a','USR-43f86a4f-0c62-4ef2-88df-74db8b1b5ab4','CUS-20250201-001',NOW() - INTERVAL '1 hour'),
('USR-CUS-95dc9cb8-3df0-4d27-b7ab-d47e9bca4153','USR-77db3a63-3b2c-4702-bd09-c6c92cb46c2b','CUS-20250201-001',NOW() - INTERVAL '2 hours'),
('USR-CUS-cb8bfb75-6b73-4d23-942d-20c188a482d1','USR-98e6e493-4974-48cf-9b2d-13fa1dbde433','CUS-20250201-001',NOW() - INTERVAL '3 hours'),
('USR-CUS-ef0d6d72-f78a-4b42-b8a7-f532e5ab0ad5','USR-d80e8a5c-57d2-4c68-824f-53a8166ce71c','CUS-20250201-001',NOW() - INTERVAL '4 hours');




-- =======================================
-- 3️⃣ Machines
-- =======================================
-- Enum MachineType:
-- BEAM = 0 | PLATE = 1 | FABRICATOR = 2

INSERT INTO "Machines" 
("Name", "Model", "Type", "CreatedDate", "CustomerReference", "Reference") 
VALUES
('V310', 'V310-PL-2022', 1, NOW() - INTERVAL '290 days', 'CUS-20250201-001', 'MAC-V310-001'),
('V630', 'V630-BE-2021', 0, NOW() - INTERVAL '285 days', 'CUS-20250201-001', 'MAC-V630-002'),
('V505', 'V505-FB-2020', 2, NOW() - INTERVAL '260 days', 'CUS-20250201-002', 'MAC-V505-003'),
('V807', 'V807-BE-2023', 0, NOW() - INTERVAL '255 days', 'CUS-20250201-002', 'MAC-V807-004'),
('V325', 'V325-PL-2021', 1, NOW() - INTERVAL '240 days', 'CUS-20250201-003', 'MAC-V325-005'),
('V200', 'V200-FB-2020', 2, NOW() - INTERVAL '230 days', 'CUS-20250201-004', 'MAC-V200-006'),
('V110', 'V110-BE-2023', 0, NOW() - INTERVAL '225 days', 'CUS-20250201-004', 'MAC-V110-007'),
('V815', 'V815-BE-2022', 0, NOW() - INTERVAL '210 days', 'CUS-20250201-005', 'MAC-V815-008'),
('V505', 'V505-FB-2023', 2, NOW() - INTERVAL '190 days', 'CUS-20250201-006', 'MAC-V505-009'),
('V310', 'V310-PL-2021', 1, NOW() - INTERVAL '185 days', 'CUS-20250201-006', 'MAC-V310-010'),
('V808', 'V808-BE-2024', 0, NOW() - INTERVAL '160 days', 'CUS-20250201-007', 'MAC-V808-011'),
('V325', 'V325-PL-2022', 1, NOW() - INTERVAL '140 days', 'CUS-20250201-008', 'MAC-V325-012'),
('V505', 'V505-FB-2023', 2, NOW() - INTERVAL '135 days', 'CUS-20250201-008', 'MAC-V505-013'),
('V807', 'V807-BE-2024', 0, NOW() - INTERVAL '120 days', 'CUS-20250201-009', 'MAC-V807-014'),
('V310', 'V310-PL-2023', 1, NOW() - INTERVAL '118 days', 'CUS-20250201-009', 'MAC-V310-015'),
('V630', 'V630-BE-2020', 0, NOW() - INTERVAL '100 days', 'CUS-20250201-010', 'MAC-V630-016'),
('V505', 'V505-FB-2020', 2, NOW() - INTERVAL '98 days', 'CUS-20250201-010', 'MAC-V505-017'),
('V325', 'V325-PL-2024', 1, NOW() - INTERVAL '95 days', 'CUS-20250201-010', 'MAC-V325-018'),
('V110', 'V110-BE-2022', 0, NOW() - INTERVAL '80 days', 'CUS-20250201-003', 'MAC-V110-019'),
('V815', 'V815-BE-2024', 0, NOW() - INTERVAL '60 days', 'CUS-20250201-002', 'MAC-V815-020');




-- =======================================
-- 4️⃣ Components (one per machine)
-- =======================================
-- Enum ComponentType:
-- MACHINE_ASSEMBLY = 0

INSERT INTO "Components"
("Name", "Type", "SerialNo", "CreatedDate", "MachineReference", "Reference")
VALUES
('Main Assembly', 0, 'SN-V310-001', NOW() - INTERVAL '290 days', 'MAC-V310-001', 'COMP-V310-001'),
('Main Assembly', 0, 'SN-V630-002', NOW() - INTERVAL '285 days', 'MAC-V630-002', 'COMP-V630-002'),
('Main Assembly', 0, 'SN-V505-003', NOW() - INTERVAL '260 days', 'MAC-V505-003', 'COMP-V505-003'),
('Main Assembly', 0, 'SN-V807-004', NOW() - INTERVAL '255 days', 'MAC-V807-004', 'COMP-V807-004'),
('Main Assembly', 0, 'SN-V325-005', NOW() - INTERVAL '240 days', 'MAC-V325-005', 'COMP-V325-005'),
('Main Assembly', 0, 'SN-V200-006', NOW() - INTERVAL '230 days', 'MAC-V200-006', 'COMP-V200-006'),
('Main Assembly', 0, 'SN-V110-007', NOW() - INTERVAL '225 days', 'MAC-V110-007', 'COMP-V110-007'),
('Main Assembly', 0, 'SN-V815-008', NOW() - INTERVAL '210 days', 'MAC-V815-008', 'COMP-V815-008'),
('Main Assembly', 0, 'SN-V505-009', NOW() - INTERVAL '190 days', 'MAC-V505-009', 'COMP-V505-009'),
('Main Assembly', 0, 'SN-V310-010', NOW() - INTERVAL '185 days', 'MAC-V310-010', 'COMP-V310-010'),
('Main Assembly', 0, 'SN-V808-011', NOW() - INTERVAL '160 days', 'MAC-V808-011', 'COMP-V808-011'),
('Main Assembly', 0, 'SN-V325-012', NOW() - INTERVAL '140 days', 'MAC-V325-012', 'COMP-V325-012'),
('Main Assembly', 0, 'SN-V505-013', NOW() - INTERVAL '135 days', 'MAC-V505-013', 'COMP-V505-013'),
('Main Assembly', 0, 'SN-V807-014', NOW() - INTERVAL '120 days', 'MAC-V807-014', 'COMP-V807-014'),
('Main Assembly', 0, 'SN-V310-015', NOW() - INTERVAL '118 days', 'MAC-V310-015', 'COMP-V310-015'),
('Main Assembly', 0, 'SN-V630-016', NOW() - INTERVAL '100 days', 'MAC-V630-016', 'COMP-V630-016'),
('Main Assembly', 0, 'SN-V505-017', NOW() - INTERVAL '98 days', 'MAC-V505-017', 'COMP-V505-017'),
('Main Assembly', 0, 'SN-V325-018', NOW() - INTERVAL '95 days', 'MAC-V325-018', 'COMP-V325-018'),
('Main Assembly', 0, 'SN-V110-019', NOW() - INTERVAL '80 days', 'MAC-V110-019', 'COMP-V110-019'),
('Main Assembly', 0, 'SN-V815-020', NOW() - INTERVAL '60 days', 'MAC-V815-020', 'COMP-V815-020');


-- =======================================
-- 4️⃣ MaintenanceRules
-- =======================================
-- Enum CounterType:
-- WORKING_HOURS = 0 | PRODUCTION_HOURS = 1 | CYCLES = 2 | STARTS = 3 | TOOL_CHANGES = 4
-- Enum ComponentType:
-- MACHINE_ASSEMBLY = 0

INSERT INTO "MaintenanceRules"
("RuleName", "IntervalValue", "CounterType", "Description", "AppliesTo", "CreatedDate", "Reference")
VALUES
('Lubricate Main Assembly', 300, 0, 'Lubricate moving joints and bearings.', 0, NOW() - INTERVAL '290 days', 'RUL-LUB-001'),
('Inspect Bolts & Screws', 200, 2, 'Check and tighten all assembly bolts and screws.', 0, NOW() - INTERVAL '270 days', 'RUL-INS-002'),
('Clean Machine Surface', 100, 1, 'Remove dust, metal shavings, and residue from main structure.', 0, NOW() - INTERVAL '250 days', 'RUL-CLN-003'),
('Alignment Check', 150, 3, 'Verify that mechanical alignment and axis positioning are within tolerance.', 0, NOW() - INTERVAL '230 days', 'RUL-ALI-004'),
('Replace Lubricant Filter', 500, 0, 'Replace lubricant filter to maintain oil flow efficiency.', 0, NOW() - INTERVAL '210 days', 'RUL-FLT-005');
