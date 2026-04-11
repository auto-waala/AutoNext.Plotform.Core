using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

-- =====================================================
--Script 8: Create Colors Table
-- =====================================================

-- Drop table if exists
DROP TABLE IF EXISTS public.colors CASCADE;

--Create colors table
CREATE TABLE public.colors(
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(50) NOT NULL,
    code VARCHAR(50) NOT NULL UNIQUE,
    hex_code VARCHAR(7), --HTML color code(e.g., #FF0000)
    rgb_value VARCHAR(50),
    description TEXT,
    display_order INTEGER NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP
);

--Create indexes
CREATE INDEX idx_colors_code ON public.colors(code);
CREATE INDEX idx_colors_is_active ON public.colors(is_active);

--Create audit trigger
CREATE TRIGGER update_colors_updated_at 
    BEFORE UPDATE ON public.colors
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

--Verify table creation
SELECT 'Colors table created successfully' as Status;