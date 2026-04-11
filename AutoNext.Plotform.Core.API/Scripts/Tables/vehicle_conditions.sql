-- =====================================================
-- Script 7: Create Vehicle Conditions Table
-- =====================================================

-- Drop table if exists
DROP TABLE IF EXISTS public.vehicle_conditions CASCADE;

-- Create vehicle conditions table
CREATE TABLE public.vehicle_conditions (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(100) NOT NULL,
    code VARCHAR(50) NOT NULL UNIQUE,
    description TEXT,
    display_order INTEGER NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP
);

-- Create indexes
CREATE INDEX idx_vehicle_conditions_code ON public.vehicle_conditions(code);
CREATE INDEX idx_vehicle_conditions_is_active ON public.vehicle_conditions(is_active);

-- Add table comments
COMMENT ON TABLE public.vehicle_conditions IS 'Vehicle condition types (New, Used, Refurbished, etc.)';

-- Create audit trigger
CREATE TRIGGER update_vehicle_conditions_updated_at 
    BEFORE UPDATE ON public.vehicle_conditions 
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Verify table creation
SELECT 'Vehicle conditions table created successfully' as Status;