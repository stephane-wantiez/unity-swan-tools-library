using System.Collections.Generic;
using System.Linq;

namespace swantiez.unity.tools.activators
{
    public class LogicalActivator : AbstractActivator, IActivable
    {
        public enum LogicalOperation { None, AND, OR };
        public LogicalOperation operationForActivation;
        public LogicalOperation operationForDeactivation;
        public bool valueForActivation;
        public bool valueForDeactivation;
        public AbstractActivator[] activatorValuesToInvert;

        private readonly Dictionary<AbstractActivator, bool> activatorsValues = new Dictionary<AbstractActivator, bool>();
        private bool firstCheckDone;
        private bool currentIsActivated;

        void Awake()
        {
            init();
            activatorsValues.Clear();
            firstCheckDone = false;
            currentIsActivated = false;
        }

        private void getTotalsForActivatorsFlags(out bool totalAndValue, out bool totalOrValue)
        {
            totalAndValue = true;
            totalOrValue = false;

            foreach (KeyValuePair<AbstractActivator, bool> activatorValue in activatorsValues)
            {
                bool value = activatorValue.Value;
                if (isActivatorValueInverted(activatorValue.Key)) value = !value;
                totalAndValue = totalAndValue && value;
                totalOrValue = totalOrValue || value;
            }
        }

        private void checkForActivation(bool totalFlagsValue)
        {
            if (valueForActivation == totalFlagsValue)
            {
                if (!firstCheckDone || !currentIsActivated)
                {
                    firstCheckDone = true;
                    currentIsActivated = true;
                    activateObjects();
                }
            }
        }

        private void checkForDeactivation(bool totalFlagsValue)
        {
            if (valueForDeactivation == totalFlagsValue)
            {
                if (!firstCheckDone || currentIsActivated)
                {
                    firstCheckDone = true;
                    currentIsActivated = false;
                    deactivateObjects();
                }
            }
        }

        private void checkFlags()
        {
            bool totalAndValue, totalOrValue;
            getTotalsForActivatorsFlags(out totalAndValue, out totalOrValue);

            switch (operationForActivation)
            {
                case LogicalOperation.AND:
                    {
                        checkForActivation(totalAndValue);
                        break;
                    }
                case LogicalOperation.OR:
                    {
                        checkForActivation(totalOrValue);
                        break;
                    }
            }

            switch (operationForDeactivation)
            {
                case LogicalOperation.AND:
                    {
                        checkForDeactivation(totalAndValue);
                        break;
                    }
                case LogicalOperation.OR:
                    {
                        checkForDeactivation(totalOrValue);
                        break;
                    }
            }
        }

        private bool isActivatorValueInverted(AbstractActivator activator)
        {
            if ((activatorValuesToInvert == null) || (activatorValuesToInvert.Length == 0)) return false;
            return activatorValuesToInvert.Contains(activator);
        }

        public void link(AbstractActivator activator)
        {
            activatorsValues.Add(activator, false);
        }

        public void activate(AbstractActivator activator)
        {
            activatorsValues[activator] = true;
            checkFlags();
        }

        public void deactivate(AbstractActivator activator)
        {
            activatorsValues[activator] = false;
            checkFlags();
        }
    }
}