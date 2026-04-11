-- =====================================================
-- Script 4: Create Transmissions Table
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
DROP TABLE IF EXISTS public.transmissions CASCADE;

-- Create transmissions table
CREATE TABLE public.transmissions (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(100) NOT NULL,
    code VARCHAR(50) NOT NULL UNIQUE,
    description TEXT,
    transmission_type VARCHAR(50), -- Manual, Automatic, CVT, DCT, etc.
    gears_count INTEGER,
    applicable_categories JSONB, -- Which categories this transmission applies to
    display_order INTEGER NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP
);

-- Create indexes
CREATE INDEX idx_transmissions_code ON public.transmissions(code);
CREATE INDEX idx_transmissions_type ON public.transmissions(transmission_type);
CREATE INDEX idx_transmissions_is_active ON public.transmissions(is_active);
CREATE INDEX idx_transmissions_gears_count ON public.transmissions(gears_count);

-- Add table comments
COMMENT ON TABLE public.transmissions IS 'Types of transmissions for vehicles';
COMMENT ON COLUMN public.transmissions.transmission_type IS 'Category of transmission (Manual, Automatic, CVT, etc.)';
COMMENT ON COLUMN public.transmissions.applicable_categories IS 'JSON array of category codes this transmission applies to';

-- Create audit trigger
CREATE TRIGGER update_transmissions_updated_at 
    BEFORE UPDATE ON public.transmissions 
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Verify table creation
SELECT 'Transmissions table created successfully' as Status;