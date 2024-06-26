﻿using MediatorOne.Common;
using MediatorOne.Features;


//Class Example for Air Traffic Control
var airplane1 = new Airplane("Barbie");
var airplane2 = new Airplane("Ken");
new AirTrafficControlTower(airplane1,airplane2);

//initial requests for landing clearance
airplane1.RequestLandingClearance();
airplane2.RequestLandingClearance();

//complete first landing
airplane1.CompleteLanding();

//re-request clearing and complete landing
airplane2.RequestLandingClearance();
airplane2.CompleteLanding();