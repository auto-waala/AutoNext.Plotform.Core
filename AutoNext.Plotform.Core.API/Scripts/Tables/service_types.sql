-- =====================================================
-- Script 19: Create Service History Types Table
-- =====================================================

-- Drop table if exists
DROP TABLE IF EXISTS public.service_types CASCADE;

-- Create service types table
CREATE TABLE public.service_types (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(100) NOT NULL,
    code VARCHAR(50) NOT NULL UNIQUE,
    category VARCHAR(50), -- Routine, Repair, Maintenance
    interval_months INTEGER,
    interval_km INTEGER,
    description TEXT,
    icon_url VARCHAR(500),
    display_order INTEGER NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP
);

-- Create indexes
CREATE INDEX idx_service_types_code ON public.service_types(code);
CREATE INDEX idx_service_types_category ON public.service_types(category);

-- Create audit trigger
CREATE TRIGGER update_service_types_updated_at 
    BEFORE UPDATE ON public.service_types 
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Verify table creation
SELECT 'Service types table created successfully' as Status;