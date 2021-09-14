
//
// ChamberLib, a cross-platform game engine
// Copyright (C) 2021 izrik and Metaphysics Industries, Inc.
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
// USA
//

using System.Collections.Generic;
using System.Linq;

namespace ChamberLib
{
    public enum Transition
    {
        Up,
        Down,
        Pressed,    //up then down
        Released,   //down then up
        Held,       //down then down
        Unheld,     //up then up
    }

    public enum AnalogTransition
    {
        Greater,
        Less,
        BecomesGreater,
        BecomesLess,
        RemainsGreater,
        RemainsLess,
    }

    public class InputMapper<TAction> : SettableActionSource<TAction>
    {
        public InputMapper()
            : this(null)
        {
        }
        public InputMapper(InputSource source)
        {
            InputSource = source;
        }

        public InputSource InputSource { get; set; }

        struct TriggerMap
        {
            public TriggerMap(AnalogInput input, float threshold, AnalogTransition transition)
            {
                Input = input;
                Threshold = threshold;
                Transition = transition;
            }

            public AnalogInput Input;
            public float Threshold;
            public AnalogTransition Transition;
        }

        Dictionary<TAction, HashSet<AnalogInput>> _analogsByAction = new Dictionary<TAction, HashSet<AnalogInput>>();
        Dictionary<TAction, HashSet<KeyValuePair<Keys, Transition>>> _keysByAction = new Dictionary<TAction, HashSet<KeyValuePair<Keys, Transition>>>();
        Dictionary<TAction, HashSet<KeyValuePair<Buttons, Transition>>> _buttonsByAction = new Dictionary<TAction, HashSet<KeyValuePair<Buttons, Transition>>>();
        Dictionary<TAction, HashSet<TriggerMap>> _triggersByAction = new Dictionary<TAction, HashSet<TriggerMap>>();
        Dictionary<TAction, HashSet<STuple<MouseButtons, Transition>>> _mouseButtonsByAction = new Dictionary<TAction, HashSet<STuple<MouseButtons, Transition>>>();

        Buttons[] SplitButton(Buttons buttons)
        {
            Buttons[] all = 
            {
                Buttons.DPadUp,
                Buttons.DPadDown,
                Buttons.DPadLeft,
                Buttons.DPadRight,
                Buttons.Start,
                Buttons.Back,
                Buttons.LeftStick,
                Buttons.RightStick,
                Buttons.LeftShoulder,
                Buttons.RightShoulder,
                Buttons.BigButton,
                Buttons.A,
                Buttons.B,
                Buttons.X,
                Buttons.Y,
                Buttons.LeftThumbstickLeft,
                Buttons.RightTrigger,
                Buttons.LeftTrigger,
                Buttons.RightThumbstickUp,
                Buttons.RightThumbstickDown,
                Buttons.RightThumbstickRight,
                Buttons.RightThumbstickLeft,
                Buttons.LeftThumbstickUp,
                Buttons.LeftThumbstickDown,
                Buttons.LeftThumbstickRight,
            };

            //TODO: re-use instead of allocating
            List<Buttons> list = new List<Buttons>();
            foreach (Buttons b in all)
            {
                if ((buttons & b) == b)
                {
                    list.Add(b);
                }
            }

            if (list.Count > 1)
            {
//                Debug.Assert(list.Count <= 1, "Multiple buttons");
            }

            return list.ToArray();
        }


        private KeyValuePair<Keys, Transition> kvp(Keys key, Transition transition)
        {
            return new KeyValuePair<Keys, Transition>(key, transition);
        }
        private KeyValuePair<Buttons, Transition> kvp(Buttons buttons, Transition transition)
        {
            return new KeyValuePair<Buttons, Transition>(buttons, transition);
        }
        private STuple<MouseButtons, Transition> stuple(MouseButtons button, Transition transition)
        {
            return new STuple<MouseButtons, Transition>(button, transition);
        }

        //TODO: do we need to split Keys, the way we split Buttons? They are both [Flags] enums.
        public void AddMap(Keys key, Transition transition, TAction action)
        {
            if (!_keysByAction.ContainsKey(action))
            {
                _keysByAction.Add(action, new HashSet<KeyValuePair<Keys, Transition>>());
            }
            _keysByAction[action].Add(kvp(key,transition));
        }

        public void AddMap(Buttons buttons, Transition transition, TAction action)
        {
            foreach (Buttons b in SplitButton(buttons))
            {
                if (!_buttonsByAction.ContainsKey(action))
                {
                    _buttonsByAction.Add(action, new HashSet<KeyValuePair<Buttons, Transition>>());
                }
                _buttonsByAction[action].Add(kvp(b,transition));
            }
        }

        public void AddMap(MouseButtons button, Transition transition, TAction action)
        {
            if (!_mouseButtonsByAction.ContainsKey(action))
            {
                _mouseButtonsByAction.Add(action, new HashSet<STuple<MouseButtons, Transition>>());
            }
            _mouseButtonsByAction[action].Add(stuple(button, transition));
        }

        public void AddMap(AnalogInput input, float threshold, AnalogTransition transition, TAction action)
        {
            if (!_triggersByAction.ContainsKey(action))
            {
                _triggersByAction.Add(action, new HashSet<TriggerMap>());
            }
            _triggersByAction[action].Add(new TriggerMap(input, threshold, transition));
        }

        public void AddAnalogMap(AnalogInput input, TAction action)
        {
            if (!_analogsByAction.ContainsKey(action))
            {
                _analogsByAction.Add(action, new HashSet<AnalogInput>());
            }
            _analogsByAction[action].Add(input);
        }

        public void RemoveMap(Keys key, Transition transition, TAction action)
        {
            if (_keysByAction.ContainsKey(action))
            {
                _keysByAction[action].Remove(kvp(key,transition));
            }
        }

        public void RemoveMap(Buttons buttons, Transition transition, TAction action)
        {
            foreach (Buttons b in SplitButton(buttons))
            {
                if (_buttonsByAction.ContainsKey(action))
                {
                    _buttonsByAction[action].Remove(kvp(b,transition));
                }
            }
        }

        public void RemoveMap(AnalogInput input, float threshold, AnalogTransition transition, TAction action)
        {
            if (_triggersByAction.ContainsKey(action))
            {
                _triggersByAction[action].Remove(new TriggerMap(input, threshold, transition));
            }
        }

        public void RemoveAnalogMap(AnalogInput input, TAction action)
        {
            if (_analogsByAction.ContainsKey(action))
            {
                _analogsByAction[action].Remove(input);
            }
        }

        public void Clear()
        {
            _keysByAction.Clear();
            _buttonsByAction.Clear();
            _analogsByAction.Clear();
            _triggersByAction.Clear();

            base.ClearAll();
        }

        public override bool Get(TAction action)
        {
            if (InputSource != null)
            {
                if (_keysByAction.ContainsKey(action))
                {
                    foreach (KeyValuePair<Keys, Transition> kvp in _keysByAction[action])
                    {
                        Keys key = kvp.Key;
                        Transition transition = kvp.Value;
                        switch (transition)
                        {
                            case Transition.Up: if (InputSource.IsKeyUp(key)) return true; break;
                            case Transition.Down: if (InputSource.IsKeyDown(key)) return true; break;
                            case Transition.Pressed: if (InputSource.IsKeyPressed(key)) return true; break;
                            case Transition.Released: if (InputSource.IsKeyReleased(key)) return true; break;
                            case Transition.Held: if (InputSource.IsKeyHeld(key)) return true; break;
                            case Transition.Unheld: if (InputSource.IsKeyUnheld(key)) return true; break;
                        }
                    }
                }
                if (_buttonsByAction.ContainsKey(action))
                {
                    foreach (KeyValuePair<Buttons, Transition> kvp in _buttonsByAction[action])
                    {
                        Buttons button = kvp.Key;
                        Transition transition = kvp.Value;
                        switch (transition)
                        {
                            case Transition.Up: if (InputSource.IsButtonUp(button)) return true; break;
                            case Transition.Down: if (InputSource.IsButtonDown(button)) return true; break;
                            case Transition.Pressed: if (InputSource.IsButtonPressed(button)) return true; break;
                            case Transition.Released: if (InputSource.IsButtonReleased(button)) return true; break;
                            case Transition.Held: if (InputSource.IsButtonHeld(button)) return true; break;
                            case Transition.Unheld: if (InputSource.IsButtonUnheld(button)) return true; break;
                        }
                    }
                }
                if (_mouseButtonsByAction.ContainsKey(action))
                {
                    foreach (var bt in _mouseButtonsByAction[action])
                    {
                        var button = bt.Item1;
                        var transition = bt.Item2;
                        switch (transition)
                        {
                            case Transition.Up: if (InputSource.IsButtonUp(button)) return true; break;
                            case Transition.Down: if (InputSource.IsButtonDown(button)) return true; break;
                            case Transition.Pressed: if (InputSource.IsButtonPressed(button)) return true; break;
                            case Transition.Released: if (InputSource.IsButtonReleased(button)) return true; break;
                            case Transition.Held: if (InputSource.IsButtonHeld(button)) return true; break;
                            case Transition.Unheld: if (InputSource.IsButtonUnheld(button)) return true; break;
                        }
                    }
                }
                if (_triggersByAction.ContainsKey(action))
                {
                    foreach (TriggerMap trigger in _triggersByAction[action])
                    {
                        float value = GetAnalogInput(InputSource, trigger.Input);
                        float pvalue = GetPreviousAnalogInput(InputSource, trigger.Input);
                        float threshold = trigger.Threshold;
                        switch (trigger.Transition)
                        {
                            case AnalogTransition.Greater:          if (value > threshold) return true; break;
                            case AnalogTransition.Less:             if (value < threshold) return true; break;
                            case AnalogTransition.BecomesGreater:    if (value > threshold && pvalue <= threshold) return true; break;
                            case AnalogTransition.BecomesLess:       if (value < threshold && pvalue >= threshold) return true; break;
                            case AnalogTransition.RemainsGreater:  if (value > threshold && pvalue > threshold) return true; break;
                            case AnalogTransition.RemainsLess:     if (value < threshold && pvalue < threshold) return true; break;
                        }
                    }
                }
            }

            return base.Get(action);
        }

        public override float GetAnalog(TAction action)
        {
            if (InputSource != null)
            {
                if (_analogsByAction.ContainsKey(action))
                {
                    foreach (AnalogInput input in _analogsByAction[action])
                    {
                        float value = GetAnalogInput(InputSource, input);

                        if (value != 0)
                        {
                            return value;
                        }
                    }
                }
            }

            return base.GetAnalog(action);
        }

        float GetAnalogInput(InputSource source, AnalogInput input)
        {
            return GetAnalogInput(source.CurrentGamepadState, input);
        }

        float GetPreviousAnalogInput(InputSource source, AnalogInput input)
        {
            return GetAnalogInput(source.PreviousGamepadState, input);
        }

        private static float GetAnalogInput(GamePadState pstate, AnalogInput input)
        {
            switch (input)
            {
                case AnalogInput.LeftStickX: return pstate.ThumbSticks.Left.X;
                case AnalogInput.LeftStickY: return pstate.ThumbSticks.Left.Y;
                case AnalogInput.RightStickX: return pstate.ThumbSticks.Right.X;
                case AnalogInput.RightStickY: return pstate.ThumbSticks.Right.Y;
                case AnalogInput.LeftTrigger: return pstate.Triggers.Left;
                case AnalogInput.RightTrigger: return pstate.Triggers.Right;
            }

            return 0;
        }

        public KeyValuePair<Keys, Transition>[] GetKeysForAction(TAction action)
        {
            return _keysByAction[action].ToArray();
        }
    }
}
