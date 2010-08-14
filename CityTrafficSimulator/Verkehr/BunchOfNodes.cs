﻿/*
 *  CityTrafficSimulator - a tool to simulate traffic in urban areas and on intersections
 *  Copyright (C) 2005-2010, Christian Schulte zu Berge
 *  
 *  This program is free software; you can redistribute it and/or modify it under the 
 *  terms of the GNU General Public License as published by the Free Software 
 *  Foundation; either version 3 of the License, or (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful, but WITHOUT ANY 
 *  WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 *  PARTICULAR PURPOSE. See the GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License along with this 
 *  program; if not, see <http://www.gnu.org/licenses/>.
 * 
 *  Web:  http://www.cszb.net
 *  Mail: software@cszb.net
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace CityTrafficSimulator.Verkehr
	{
	/// <summary>
	/// Ansammlung von LineNodes, die unter einem Namen zusammengefasst wird
	/// </summary>
	[Serializable]
	public class BunchOfNodes : ISavable
		{
		#region Klassenmember

		/// <summary>
		/// Name dieser Ansammlung von LineNodes
		/// </summary>
		private string m_title;
		/// <summary>
		/// Name dieser Ansammlung von LineNodes
		/// </summary> 
		public string title
			{
			get { return m_title; }
			set { m_title = value; }
			}

		/// <summary>
		/// Liste der teilnehmenden LineNodes
		/// </summary>
		[XmlIgnore]
		private List<LineNode> m_nodes = new List<LineNode>();
		/// <summary>
		/// Liste der teilnehmenden LineNodes
		/// </summary>
		[XmlIgnore]
		public List<LineNode> nodes
			{
			get { return m_nodes; }
			set { m_nodes = value; }
			}

		#endregion

		#region Konstruktoren & Methoden

		/// <summary>
		/// Erstellt eine neue leere BunchOfNodes mit dem Titel title
		/// </summary>
		/// <param name="title">Titel dieser Ansammlung von Nodes</param>
		public BunchOfNodes(string title)
			{
			this.m_title = title;
			}

		#endregion


		#region ISavable Member

		/// <summary>
		/// Hashwerte der Startknoten (nur für XML-Serialisierung benötigt)
		/// </summary>
		public List<int> nodeHashes = new List<int>();

		/// <summary>
		/// Bereitet den Fahrauftrag für die XML-Serialisierung vor
		/// </summary>
		public void PrepareForSave()
			{
			nodeHashes.Clear();
			foreach (LineNode ln in m_nodes)
				nodeHashes.Add(ln.GetHashCode());
			}

		/// <summary>
		/// Stellt die Referenzen auf LineNodes wieder her
		/// (auszuführen nach XML-Deserialisierung)
		/// </summary>
		/// <param name="saveVersion">Version der gespeicherten Datei</param>
		/// <param name="nodesList">Liste der bereits wiederhergestellten LineNodes</param>
		public void RecoverFromLoad(int saveVersion, List<LineNode> nodesList)
			{
			foreach (LineNode ln in nodesList)
				{
				if (nodeHashes.Contains(ln.GetHashCode()))
					m_nodes.Add(ln);
				}
			}

		#endregion
		}
	}