-- =======================================
-- SEED SCRIPT: Maintenance Manager (Dynamic NOW)
-- =======================================
-- Includes: Customers, Machines, MaintenanceRules
-- =======================================

-- 1️⃣ Clear existing data (in correct order)
TRUNCATE TABLE 
  "MaintenanceRules",
  "Machines",
  "Customers"
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
