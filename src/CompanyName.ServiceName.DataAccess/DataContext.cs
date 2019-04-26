using CompanyName.ServiceName.Contracts.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CompanyName.ServiceName.DataAccess
{
    public sealed class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<ParentOrganization> ParentOrganizations { get; set; }

        public DbSet<ChildOrganization> ChildOrganizations { get; set; }

        public DbSet<ConsumptionObject> ConsumptionObjects { get; set; }

        public DbSet<PowerSupplyPoint> PowerSupplyPoints { get; set; }

        public DbSet<PowerMeasurementPoint> PowerMeasurementPoints { get; set; }

        public DbSet<CalculationMeteringDevice> CalculationMeteringDevices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetManyToManyRel(modelBuilder);

            SetOwnedTypes(modelBuilder);

            SeedData(modelBuilder);
        }

        private static void SetManyToManyRel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PowerMeasurementPointCalculationMeteringDevice>(b =>
            {
                b.HasKey(x =>
                    new { x.PowerMeasurementPointId, x.CalculationMeteringDeviceId });

                b.HasOne(x => x.PowerMeasurementPoint)
                    .WithMany(pmp =>
                        pmp.PowerMeasurementPointCalculationMeteringDevice)
                    .HasForeignKey(x => x.PowerMeasurementPointId);

                b.HasOne(x => x.CalculationMeteringDevice)
                    .WithMany(pmp =>
                        pmp.PowerMeasurementPointCalculationMeteringDevice)
                    .HasForeignKey(x => x.CalculationMeteringDeviceId);
            });
        }

        private static void SetOwnedTypes(ModelBuilder modelBuilder)
        {
            // https://stackoverflow.com/questions/50862525/seed-entity-with-owned-property
            modelBuilder.Entity<PowerMeasurementPoint>()
                .OwnsOne(pmp => pmp.ElectricityMeter, x =>
                    {
                        x.HasIndex(em => em.Number).IsUnique();
                        x.HasIndex(em => em.VerificationDate);
                        x.HasIndex(em => new { em.Number, em.VerificationDate }).IsUnique();
                        x.HasData(
                            new
                            {
                                PowerMeasurementPointId = 1L,
                                Number = 1L,
                                VerificationDate = new DateTime(2018, 1, 3),
                                MeterType = "MeterType1"
                            },
                            new
                            {
                                PowerMeasurementPointId = 2L,
                                Number = 2L,
                                VerificationDate = new DateTime(2018, 2, 4),
                                MeterType = "MeterType2"
                            });
                    });

            modelBuilder.Entity<PowerMeasurementPoint>()
                .OwnsOne(pmp => pmp.CurrentTransformer, x =>
                    {
                        x.HasIndex(em => em.Number).IsUnique();
                        x.HasIndex(em => em.VerificationDate);
                        x.HasIndex(em => new { em.Number, em.VerificationDate }).IsUnique();
                        x.HasData(
                            new
                            {
                                PowerMeasurementPointId = 1L,
                                Number = 1L,
                                VerificationDate = new DateTime(2018, 1, 3),
                                Type = "CurrentTransformerType1",
                                TransformationRatio = 1.1
                            },
                            new
                            {
                                PowerMeasurementPointId = 2L,
                                Number = 2L,
                                VerificationDate = new DateTime(2018, 2, 4),
                                Type = "CurrentTransformerType2",
                                TransformationRatio = 2.2
                            });
                    });

            modelBuilder.Entity<PowerMeasurementPoint>()
                .OwnsOne(pmp => pmp.VoltageTransformer, x =>
                    {
                        x.HasIndex(em => em.Number).IsUnique();
                        x.HasIndex(em => em.VerificationDate);
                        x.HasIndex(em => new { em.Number, em.VerificationDate }).IsUnique();
                        x.HasData(
                            new
                            {
                                PowerMeasurementPointId = 1L,
                                Number = 1L,
                                VerificationDate = new DateTime(2018, 1, 3),
                                Type = "VoltageTransformerType1",
                                TransformationRatio = 1.1
                            },
                            new
                            {
                                PowerMeasurementPointId = 2L,
                                Number = 2L,
                                VerificationDate = new DateTime(2018, 2, 4),
                                Type = "VoltageTransformerType2",
                                TransformationRatio = 2.2
                            });
                    });
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            var parents = new List<ParentOrganization>
            {
                new ParentOrganization { Id = 1, Name = "Parent1", Address = "ParAddress1" },
                new ParentOrganization { Id = 2, Name = "Parent2", Address = "ParAddress2" }
            };

            var children = new ArrayList
            {
                new
                {
                    Id = 1,
                    ParentOrganizationId = 1,
                    Name = "Child1",
                    Address = "ChildAddress1"
                },
                new
                {
                    Id = 2,
                    ParentOrganizationId = 2,
                    Name = "Child2",
                    Address = "ChildAddress2"
                }
            };

            var consumers = new ArrayList
            {
                new
                {
                    Id = 1,
                    OrganizationId = 1,
                    Name = "ПС 110/10 Весна",
                    Address = "ConsAddress1"
                },
                new
                {
                    Id = 2,
                    OrganizationId = 2,
                    Name = "Cons2",
                    Address = "Летово 110/10 кВ Россеть"
                }
            };

            var suppliers = new ArrayList
            {
                new
                {
                    Id = 1L,
                    ConsumptionObjectId = 1,
                    Name = "Supp1",
                    MaximumPower = 110.0
                },
                new
                {
                    Id = 2L,
                    ConsumptionObjectId = 2,
                    Name = "Supp2",
                    MaximumPower = 220.0
                }
            };

            var measurers = new ArrayList
            {
                new
                {
                    Id = 1L, ConsumptionObjectId = 1, Name = "Meas1"
                },
                new
                {
                    Id = 2L, ConsumptionObjectId = 2, Name = "Meas2"
                }
            };

            var calcDevices = new ArrayList { new { Id = 1L }, new { Id = 2L }, new { Id = 3L } };

            // PowerMeasurementPointCalculationMeteringDevice
            var pmpcmd = new List<PowerMeasurementPointCalculationMeteringDevice>
            {
                new PowerMeasurementPointCalculationMeteringDevice
                {
                    CalculationMeteringDeviceId = 1L,
                    PowerMeasurementPointId = 1L,
                    StartPeriod = new DateTime(2017, 1, 1),
                    EndPeriod = new DateTime(2017, 7, 1)
                },
                new PowerMeasurementPointCalculationMeteringDevice
                {
                    CalculationMeteringDeviceId = 2L,
                    PowerMeasurementPointId = 2L,
                    StartPeriod = new DateTime(2017, 7, 1),
                    EndPeriod = new DateTime(2018, 1, 1)
                },
                new PowerMeasurementPointCalculationMeteringDevice
                {
                    CalculationMeteringDeviceId = 1L,
                    PowerMeasurementPointId = 2L,
                    StartPeriod = new DateTime(2018, 1, 1),
                    EndPeriod = new DateTime(2018, 7, 1)
                },
                new PowerMeasurementPointCalculationMeteringDevice
                {
                    CalculationMeteringDeviceId = 2L,
                    PowerMeasurementPointId = 1L,
                    StartPeriod = new DateTime(2018, 7, 1),
                    EndPeriod = new DateTime(2019, 1, 1)
                },
                new PowerMeasurementPointCalculationMeteringDevice
                {
                    CalculationMeteringDeviceId = 3L,
                    PowerMeasurementPointId = 1L,
                    StartPeriod = new DateTime(2019, 1, 1),
                    EndPeriod = new DateTime(2019, 7, 1)
                },
                new PowerMeasurementPointCalculationMeteringDevice
                {
                    CalculationMeteringDeviceId = 3L,
                    PowerMeasurementPointId = 2L,
                    StartPeriod = new DateTime(2017, 12, 1),
                    EndPeriod = new DateTime(2019, 1, 1)
                },
            };

            modelBuilder.Entity<ParentOrganization>()
                .HasData(parents.ToArray());
            modelBuilder.Entity<ChildOrganization>()
                .HasData(children[0], children[1]);
            modelBuilder.Entity<ConsumptionObject>()
                .HasData(consumers[0], consumers[1]);
            modelBuilder.Entity<PowerSupplyPoint>()
                .HasData(suppliers[0], suppliers[1]);
            modelBuilder.Entity<PowerMeasurementPoint>()
                .HasData(measurers[0], measurers[1]);
            modelBuilder.Entity<CalculationMeteringDevice>()
                .HasData(calcDevices[0], calcDevices[1], calcDevices[2]);
            modelBuilder.Entity<PowerMeasurementPointCalculationMeteringDevice>()
                .HasData(pmpcmd[0], pmpcmd[1], pmpcmd[2], pmpcmd[3], pmpcmd[4], pmpcmd[5]);
        }
    }
}
