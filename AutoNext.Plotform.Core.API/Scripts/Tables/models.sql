-- =====================================================
-- Script 6: Create Models Table
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
DROP TABLE IF EXISTS public.models CASCADE;

-- Create models table
CREATE TABLE public.models (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    brand_id UUID NOT NULL,
    name VARCHAR(100) NOT NULL,
    code VARCHAR(50) NOT NULL,
    slug VARCHAR(100) NOT NULL,
    description TEXT,
    vehicle_type_id UUID NOT NULL,
    start_year INTEGER,
    end_year INTEGER,
    is_current_model BOOLEAN DEFAULT false,
    image_url VARCHAR(500),
    display_order INTEGER NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT true,
    metadata JSONB,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP,

    CONSTRAINT fk_models_brand FOREIGN KEY (brand_id)
        REFERENCES public.brands(id) ON DELETE CASCADE,
    CONSTRAINT fk_models_vehicle_type FOREIGN KEY (vehicle_type_id)
        REFERENCES public.vehicle_types(id) ON DELETE RESTRICT,
    CONSTRAINT unique_brand_model_code UNIQUE (brand_id, code),
    CONSTRAINT unique_brand_model_name UNIQUE (brand_id, name)
);

-- Create indexes
CREATE INDEX idx_models_brand ON public.models(brand_id);
CREATE INDEX idx_models_vehicle_type ON public.models(vehicle_type_id);
CREATE INDEX idx_models_slug ON public.models(slug);
CREATE INDEX idx_models_is_active ON public.models(is_active);
CREATE INDEX idx_models_years ON public.models(start_year, end_year);

-- Add table comments
COMMENT ON TABLE public.models IS 'Vehicle models under each brand';
COMMENT ON COLUMN public.models.start_year IS 'First year of production';
COMMENT ON COLUMN public.models.end_year IS 'Last year of production (NULL if still in production)';
COMMENT ON COLUMN public.models.vehicle_type_id IS 'Reference to vehicle_types table';
COMMENT ON COLUMN public.models.brand_id IS 'Reference to brands table';

-- Create audit trigger
CREATE TRIGGER update_models_updated_at
    BEFORE UPDATE ON public.models
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Verify table creation
SELECT 'Models table created successfully' AS Status;