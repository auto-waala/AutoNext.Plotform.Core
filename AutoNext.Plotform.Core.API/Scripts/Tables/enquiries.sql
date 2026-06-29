-- =====================================================
-- Script 9: Create Enquiries Table
-- =====================================================

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

DROP TABLE IF EXISTS public.enquiries CASCADE;

CREATE TABLE public.enquiries
(
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),

    name VARCHAR(100) NOT NULL,
    email VARCHAR(150) NOT NULL,
    phone VARCHAR(20) NOT NULL,

    category VARCHAR(100) NOT NULL,

    city VARCHAR(100),

    message TEXT,

    status VARCHAR(30) NOT NULL DEFAULT 'Pending',

    is_active BOOLEAN NOT NULL DEFAULT true,

    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP
);

CREATE INDEX idx_enquiries_name ON public.enquiries(name);
CREATE INDEX idx_enquiries_email ON public.enquiries(email);
CREATE INDEX idx_enquiries_phone ON public.enquiries(phone);
CREATE INDEX idx_enquiries_category ON public.enquiries(category);
CREATE INDEX idx_enquiries_status ON public.enquiries(status);

COMMENT ON TABLE public.enquiries IS 'Customer enquiries for vehicles';

COMMENT ON COLUMN public.enquiries.category IS
'Selected category such as Cars, Bikes, Trucks, etc.';

CREATE TRIGGER update_enquiries_updated_at
BEFORE UPDATE ON public.enquiries
FOR EACH ROW
EXECUTE FUNCTION update_updated_at_column();

SELECT 'Enquiries table created successfully' AS Status;