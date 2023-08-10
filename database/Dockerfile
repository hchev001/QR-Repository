# Use the official PostgreSQL image
FROM postgres:latest

# Set environment variables for PostgreSQL
ENV POSTGRES_USER joe
ENV POSTGRES_PASSWORD pass12345
ENV POSTGRES_DB inventory

# Copy initialization scripts to the container
# COPY init.sql /docker-entrypoint-initdb.d/