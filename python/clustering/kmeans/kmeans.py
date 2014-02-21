import random

class Cluster(object):
    id = 0
    def __init__(self, center):
        self.id = Cluster.id
        Cluster.id += 1
        self.center = center
        self.prevCenter = None
    def _clearObservations(self):
        self.observations = [] 
    def _addObservation(self, obs):
        self.observations.append(obs)
    def computeError(self, distanceFunc, featureVectorFunc):
        error = 0
        for obs in self.observations:
            error += distanceFunc(self.center, featureVectorFunc(obs))
        return error
    def getCenter(self):
        return self.center
    def _updateCenter(self, centerFunc, featureVectorFunc):
        self.prevCenter = self.center
        self.center = centerFunc(map(lambda obs: featureVectorFunc(obs) ,self.observations))
    def _centerChanged(self, distanceFunc):
        if self.prevCenter == None:
            return True
        return distanceFunc(self.prevCenter, self.center) != 0

def cluster(observations, numMeans, featureVectorFunc, maxIterations):
    """K-Means Clustering 
    observations: List of observations to cluster. 
    numMeans: Number of clusters desired.
    featureVectorFunc: Function that takes in an observation and returns a feature vector in the form of a Tuple.
    maxIterations: Optional parameter used to constrain the number of iterations the alorithm can run for.
    """
    clusters = initializeClusters(observations, numMeans)
    iterations = 0
    hasConverged = False
    while not hasConverged and (iterations < maxIterations or maxIterations == None):
        iterations = iterations + 1
        # cluster assignment step
        for cluster in clusters:
            cluster._clearObservations()
        for obs in observations:
            closestCluster = computeClosestCluster(obs, clusters, featureVectorFunc)
            closestCluster._addObservation(obs)
        # update cluster centers 
        for cluster in clusters:
            cluster._updateCenter(centerFunc, featureVectorFunc)
        hasConverged = not clustersWereUpdated(clusters, distanceFunc)
    
    totalError = 0
    for cluster in clusters:
        totalError += cluster.computeError(distanceFunc, featureVectorFunc)
    return [clusters, totalError, iterations]    

def initializeClusters(observations, numMeans):
    clusters = []
    random.shuffle(observations)
    # randomly choose k initial clusters
    for i in range(numMeans):
        clusters.append(Cluster(observations[i]))
    return clusters 

def clustersWereUpdated(clusters, distanceFunc):
    wereUpdated = False
    for cluster in clusters:
        wereUpdated |= cluster._centerChanged(distanceFunc)
    return wereUpdated

def computeClosestCluster(observation, clusters, featureVectorFunc):
    closestDist = float("inf") 
    closestCluster = None
    for cluster in clusters:
        distance = distanceFunc(featureVectorFunc(observation), cluster.center)
        if distance < closestDist:
            closestDist = distance
            closestCluster = cluster        
    return closestCluster

def distanceFunc(x, y):
    return sum([(a-b)**2 for a,b in zip(x,y)])

def centerFunc(items):
    arrangeByDimensions = zip(*map(lambda x: x, items))
    averageLocation = []
    for dimVals in arrangeByDimensions:
        averageLocation.append(sum(dimVals) / len(dimVals))
    return tuple(averageLocation)
