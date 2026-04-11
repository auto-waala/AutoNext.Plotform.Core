-- =====================================================
-- Script 3: Create Fuel Types Table
-- =====================================================

-- Create the trigger function first (if it doesn't already exist)
CREATE OR REPLACE FUNCTION update_updated_at_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Drop table if exists
DROP TABLE IF EXISTS public.fuel_types CASCADE;

-- Create fuel types table
CREATE TABLE public.fuel_types (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(100) NOT NULL,
    code VARCHAR(50) NOT NULL UNIQUE,
    description TEXT,
    icon_url VARCHAR(500),
    applicable_categories JSONB,
    display_order INTEGER NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP
);

-- Create indexes
CREATE INDEX idx_fuel_types_code ON public.fuel_types(code);
CREATE INDEX idx_fuel_types_is_active ON public.fuel_types(is_active);

-- Add table comments
COMMENT ON TABLE public.fuel_types IS 'Types of fuel for motor vehicles';
COMMENT ON COLUMN public.fuel_types.applicable_categories IS 'JSON array of category codes this fuel type applies to';

-- Create audit trigger
CREATE TRIGGER update_fuel_types_updated_at 
    BEFORE UPDATE ON public.fuel_types 
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Verify table creation
SELECT 'Fuel types table created successfully' as Status;