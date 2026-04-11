-- =====================================================
-- Script 24: Create Inspection Checklist Table
-- =====================================================

-- Ensure the trigger function exists (safe to re-run)
CREATE OR REPLACE FUNCTION update_updated_at_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Drop table if exists
DROP TABLE IF EXISTS public.inspection_checklist CASCADE;

-- Create inspection checklist table
CREATE TABLE public.inspection_checklist (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(100) NOT NULL,
    code VARCHAR(50) NOT NULL UNIQUE,
    category VARCHAR(50),                  -- Engine, Exterior, Interior, Electrical, etc.
    applicable_vehicle_types JSONB,        -- JSON array e.g. '["BIKES", "CYCLES", "EV"]'
    is_critical BOOLEAN DEFAULT false,
    weightage INTEGER DEFAULT 1,           -- Importance weightage (1-10)
    display_order INTEGER NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP
);

-- Create indexes
CREATE INDEX idx_inspection_checklist_code         ON public.inspection_checklist(code);
CREATE INDEX idx_inspection_checklist_category     ON public.inspection_checklist(category);
CREATE INDEX idx_inspection_checklist_is_active    ON public.inspection_checklist(is_active);
CREATE INDEX idx_inspection_checklist_is_critical  ON public.inspection_checklist(is_critical);
CREATE INDEX idx_inspection_checklist_veh_types    ON public.inspection_checklist USING GIN(applicable_vehicle_types);

-- Add table comments
COMMENT ON TABLE public.inspection_checklist IS 'Inspection checklist items for vehicle quality checks';
COMMENT ON COLUMN public.inspection_checklist.applicable_vehicle_types IS 'JSON array of vehicle type codes this check applies to. NULL means applies to all.';
COMMENT ON COLUMN public.inspection_checklist.weightage IS 'Importance score from 1 (low) to 10 (high)';
COMMENT ON COLUMN public.inspection_checklist.is_critical IS 'If true, failure on this item is a deal-breaker';

-- Create audit trigger
CREATE TRIGGER update_inspection_checklist_updated_at
    BEFORE UPDATE ON public.inspection_checklist
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Verify table creation
SELECT 'Inspection checklist table created successfully' AS Status;