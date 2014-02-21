import math

class PrimeSieve():
    def generatePrimes(self, maxValue):
        numbers = range(0, maxValue + 1)
        primes = list()
        for candidateNumber in range(2, maxValue + 1):
            if(numbers[candidateNumber] != None):
                primes.append(candidateNumber)
            # cancel all multiples of the candidateNumber (they can't be prime)
            numberToCancel = candidateNumber
            while numberToCancel <= maxValue:
                numbers[numberToCancel] = None
                numberToCancel = numberToCancel + candidateNumber
        return primes