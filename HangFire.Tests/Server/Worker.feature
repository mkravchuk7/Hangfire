﻿@redis
Feature: Worker

Scenario: Worker should perform a job
    Given an enqueued job
     When the worker processes the next job
     Then the job should be performed

Scenario: Successfully performed job should have the Succeeded state
    Given an enqueued job
     When the worker processes the next job
     Then its state should be Succeeded
      And the job should be removed from the dequeued list

Scenario: After performing the broken job, it should have the Failed state
    Given an enqueued broken job
     When the worker processes the next job
     Then its state should be Failed
      And the job should be removed from the dequeued list

Scenario: An unexisting job should not be processed, but it should be removed from the dequeued list
    Given the 'unexisting' job, that was enqueued
     When the worker processes the next job
     Then there should be no performing actions
      But the 'unexisting' job should be removed from the dequeued list
      
Scenario: Worker should processes only jobs in the Enqueued state, but it should remove the job from the dequeued list anyway
    Given an enqueued job
      And its state is Processing
     When the worker processes the next job
     Then the job should not be performed
      But it should be removed from the dequeued list 

Scenario: Disposable job should be disposed after processing
    Given an enqueued job
     When the worker processes the next job
     Then the job should be disposed