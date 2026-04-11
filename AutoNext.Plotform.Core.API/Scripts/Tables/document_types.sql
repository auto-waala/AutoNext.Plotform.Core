-- =====================================================
-- Script 23: Create Document Types Table
-- =====================================================

-- Drop table if exists
DROP TABLE IF EXISTS public.document_types CASCADE;

-- Create document types table
CREATE TABLE public.document_types (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(100) NOT NULL,
    code VARCHAR(50) NOT NULL UNIQUE,
    category VARCHAR(50), -- Vehicle, Seller, Buyer, Transaction
    is_required BOOLEAN DEFAULT false,
    is_verifiable BOOLEAN DEFAULT false,
    expiry_months INTEGER,
    applicable_vehicle_types JSONB,
    display_order INTEGER NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP
);

-- Create indexes
CREATE INDEX idx_document_types_code ON public.document_types(code);
CREATE INDEX idx_document_types_category ON public.document_types(category);

-- Create audit trigger
CREATE TRIGGER update_document_types_updated_at 
    BEFORE UPDATE ON public.document_types 
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Verify table creation
SELECT 'Document types table created successfully' as Status;