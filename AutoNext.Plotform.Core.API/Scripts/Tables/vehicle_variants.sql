using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.IO;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

-- =====================================================
--Script 10: Create Vehicle Variants/Trims Table
-- =====================================================

-- Drop table if exists
DROP TABLE IF EXISTS public.vehicle_variants CASCADE;

--Create vehicle variants table
CREATE TABLE public.vehicle_variants(
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    model_id UUID NOT NULL,
    name VARCHAR(100) NOT NULL, --e.g., "LX", "EX", "Touring", "Sport"
    code VARCHAR(50) NOT NULL,
    description TEXT,
    fuel_type_id UUID,
    transmission_id UUID,
    drive_type VARCHAR(50), --FWD, RWD, AWD, 4WD
    engine_size DECIMAL(5, 2),
    horsepower INTEGER,
    torque INTEGER,
    seating_capacity INTEGER,
    doors_count INTEGER,
    base_price DECIMAL(12, 2),
    is_available BOOLEAN DEFAULT true,
    display_order INTEGER NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT true,
    metadata JSONB,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP,

    CONSTRAINT fk_variants_model FOREIGN KEY(model_id)
        REFERENCES public.models(id) ON DELETE CASCADE,
    CONSTRAINT fk_variants_fuel_type FOREIGN KEY (fuel_type_id) 
        REFERENCES public.fuel_types(id) ON DELETE SET NULL,
    CONSTRAINT fk_variants_transmission FOREIGN KEY (transmission_id) 
        REFERENCES public.transmissions(id) ON DELETE SET NULL,
    CONSTRAINT unique_model_variant UNIQUE (model_id, code)
);

--Create indexes
CREATE INDEX idx_variants_model ON public.vehicle_variants(model_id);
CREATE INDEX idx_variants_fuel_type ON public.vehicle_variants(fuel_type_id);
CREATE INDEX idx_variants_transmission ON public.vehicle_variants(transmission_id);
CREATE INDEX idx_variants_is_active ON public.vehicle_variants(is_active);

--Add table comments
COMMENT ON TABLE public.vehicle_variants IS 'Different trim levels/variants for each model';

--Create audit trigger
CREATE TRIGGER update_variants_updated_at 
    BEFORE UPDATE ON public.vehicle_variants
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

--Verify table creation
SELECT 'Vehicle variants table created successfully' as Status;