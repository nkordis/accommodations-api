# Accommodations Domain

## Overview

This directory contains the domain entities for the Accommodations API project. The domain layer is responsible for the core business logic and domain models.

## Domain Entities

### Accommodation

Represents the overall accommodation entity such as a hotel, resort, or apartment complex.

- **Properties**:
  - `Id`: Unique identifier for the accommodation.
  - `Name`: Name of the accommodation.
  - `Description`: Detailed description of the accommodation.
  - `Type`: Type of the accommodation (e.g., Hotel, Resort, BedAndBreakfast, Hostel, IndividualOwner, GuestHouse, ApartmentComplex, Other).
  - `HasInstantBooking`: Indicates whether the accommodation supports instant booking.
  - `ContactEmail`: Contact email for the accommodation.
  - `ContactNumber`: Contact phone number for the accommodation.
  - `Address`: Address of the accommodation.
  - `Units`: List of units available within the accommodation.

### Unit

Represents an individual rentable space within an accommodation.

- **Properties**:
  - `Id`: Unique identifier for the unit.
  - `Name`: Name of the unit.
  - `Description`: Detailed description of the unit.
  - `Price`: Price of the unit.
  - `BillingPeriod`: Billing period for the unit (e.g., PerDay, PerWeek, PerMonth, PerYear).
  - `Capacity`: Capacity of the unit.
  - `Type`: Type of the unit (e.g., Room, Suite, Apartment, Villa, Cottage, Dormitory, Other).
  - `Address`: Address of the unit.
  - `AccommodationId`: Identifier for the parent accommodation.

### Address

Represents an address.

- **Properties**:
  - `City`: City of the address.
  - `Street`: Street of the address.
  - `PostalCode`: Postal code of the address.

## Enums

### BillingPeriod

Enumeration representing the billing period for a unit.

- **Values**:
  - `PerDay`
  - `PerWeek`
  - `PerMonth`
  - `PerYear`

### AccommodationType

Enumeration representing different types of accommodations.

- **Values**:
  - `Hotel`
  - `Resort`
  - `BedAndBreakfast`
  - `Hostel`
  - `IndividualOwner`
  - `GuestHouse`
  - `ApartmentComplex`
  - `Other`

### UnitType

Enumeration representing different types of units within an accommodation.

- **Values**:
  - `Room`
  - `Suite`
  - `Apartment`
  - `Villa`
  - `Cottage`
  - `Dormitory`
  - `Other`
