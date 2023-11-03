using Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem;

var prescriberFacade = new PrescriberFacade();
prescriberFacade.LoadPatientDataFrom("PatientData.json");
prescriberFacade.LoadPotentialDiseaseDataFrom("PotentialDisease.txt");

prescriberFacade.Prescribe("A123456789", new[] {"Headache", "Cough"});
prescriberFacade.Prescribe("B123456789", new[] {"Sneeze"});
prescriberFacade.Prescribe("C555555555", new[] {"Snore"});